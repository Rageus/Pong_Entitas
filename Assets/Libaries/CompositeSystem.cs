using System;
using System.Collections.Generic;
using Entitas;



/*
    Late reactive systems are guaranteed to run AFTER normal reactive systems,
    but before cleanup systems.
    Avoid using this, unless you have a good reason!
    DO NOT USE THIS ON THE SERVER!
*/
public class AddLateReactSystemsSystem : CompositeSystem {
    public AddLateReactSystemsSystem(Contexts contexts) : base(contexts) {

        AddInitialize(() => {
            foreach (var addSystemCallback in _AddReactEachLateActions.addSystemActions) {
                addSystemCallback.Invoke();
            }
            _AddReactEachLateActions.addSystemActions = null;
        });
    }
}


// ==== Hack ====
// static class to make sure addreacteachlate systems are added before other systems initialize
public static class _AddReactEachLateActions {
    public static List<Action> addSystemActions = new List<Action>();
}


public class CompositeSystem : Systems {
    IContexts contexts;

    public CompositeSystem(IContexts contexts) {
        this.contexts = contexts;
    }



    public void AddInitialize(Action initialize) {
        Add(new InlineInitializeSystem(initialize));
    }

    public void AddExecute(Action execute) {
        Add(new InlineExecuteSystem(execute));
    }

    public void AddCleanup(Action cleanup) {
        Add(new InlineCleanupSystem(cleanup));
    }

    public void AddTeardown(Action tearDown) {
        Add(new InlineTeardownSystem(tearDown));
    }



    public enum Filter {
        Auto,
        Removed,
        None
    }



    public void AddReact<TEntity>(IMatcher<TEntity> matcher, Filter filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        Func<TEntity, bool> predicate;
        switch (filter) {
            case Filter.Auto:
                predicate = matcher.Matches;
                break;
            case Filter.None:
                predicate = e => true;
                break;
            case Filter.Removed:
                throw new Exception("Filter.Removed should only be used with .Removed() eventTriggers");
            default:
                throw new ArgumentOutOfRangeException("filter", filter, null);
        }
        AddReact(matcher, predicate, execute);
    }

    public void AddReact<TEntity>(IMatcher<TEntity> matcher, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        AddReact(GetContext<TEntity>().CreateCollector(matcher), filter, execute);
    }

    public void AddReact<TEntity>(TriggerOnEvent<TEntity> trigger, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        AddReact(new [] { trigger }, filter, execute);
    }

    public void AddReact<TEntity>(TriggerOnEvent<TEntity> trigger, Filter filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
       

        Func<TEntity, bool> predicate;
        switch (filter) {
            case Filter.Auto:
                if (trigger.groupEvent != GroupEvent.Added)
                    throw new Exception("Filter.Auto needs .Added() trigger");
                predicate = e => trigger.matcher.Matches(e);
                break;
            case Filter.Removed:
                if(trigger.groupEvent != GroupEvent.Removed)
                    throw new Exception("Filter.Removed needs .Removed() trigger");
                predicate = e => !trigger.matcher.Matches(e);
                break;
            case Filter.None:
                predicate = e => true;
                break;
            default:
                throw new ArgumentOutOfRangeException("filter", filter, null);
        }
        
        AddReact(trigger,predicate,execute);
    }

    public void AddReact<TEntity>(TriggerOnEvent<TEntity>[] triggers, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        AddReact(GetContext<TEntity>().CreateCollector(triggers), filter, execute);
    }

    public void AddReact<TEntity>(ICollector<TEntity> collector, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        Add(new InlineReactiveSystem<TEntity>(collector, filter, execute));
    }

    public void AddReactEach<TEntity>(IMatcher<TEntity> matcher, Filter filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReact(matcher, filter, entities => entities.ForEach(execute));
    }

    public void AddReactEach<TEntity>(IMatcher<TEntity> matcher, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReact(matcher, filter, entities => entities.ForEach(execute));
    }

    public void AddReactEach<TEntity>(TriggerOnEvent<TEntity> trigger, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReact(trigger, filter, entities => entities.ForEach(execute));
    }

    public void AddReactEach<TEntity>(TriggerOnEvent<TEntity> trigger, Filter filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReact(trigger, filter, entities => entities.ForEach(execute));
    }

    public void AddReactEach<TEntity>(TriggerOnEvent<TEntity>[] triggers, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReact(triggers, filter, entities => entities.ForEach(execute));
    }

    public void AddReactEach<TEntity>(ICollector<TEntity> collector, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReact(collector, filter, entities => entities.ForEach(execute));
    }




    /*

        LATE REACT

    */


    public void AddReactLate<TEntity>(IMatcher<TEntity> matcher, Filter filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        if (filter == Filter.Removed) throw new Exception("Filter.Removed should only be used with .Removed() eventTriggers");
        AddReactLate(matcher, e => filter == Filter.None || matcher.Matches(e), execute);
    }
    public void AddReactLate<TEntity>(IMatcher<TEntity> matcher, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        AddReactLate(GetContext<TEntity>().CreateCollector(matcher), filter, execute);
    }
    public void AddReactLate<TEntity>(TriggerOnEvent<TEntity> trigger, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        AddReactLate(new [] { trigger }, filter, execute);
    }
    public void AddReactLate<TEntity>(TriggerOnEvent<TEntity> trigger, Filter filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        if (filter == Filter.Removed && trigger.groupEvent != GroupEvent.Removed)
            throw new Exception("Filter.Removed needs .Removed() trigger");
        if (filter == Filter.Auto && trigger.groupEvent != GroupEvent.Added)
            throw new Exception("Filter.Auto needs .Added() trigger");
        AddReactLate(
            trigger,
            e => {
                if (filter == Filter.Auto) return trigger.matcher.Matches(e);
                if (filter == Filter.Removed) return !trigger.matcher.Matches(e);
                if (filter == Filter.None) return true;
                throw new Exception("Should never reach here");
            },
            execute
        );
    }
    public void AddReactLate<TEntity>(TriggerOnEvent<TEntity>[] triggers, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        AddReactLate(GetContext<TEntity>().CreateCollector(triggers), filter, execute);
    }
    public void AddReactLate<TEntity>(ICollector<TEntity> collector, Func<TEntity, bool> filter, Action<List<TEntity>> execute) where TEntity : class, IEntity {
        _AddReactEachLateActions.addSystemActions.Add(() => Add(new InlineReactiveSystem<TEntity>(collector, filter, execute)));
    }




    public void AddReactEachLate<TEntity>(IMatcher<TEntity> matcher, Filter filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReactLate(matcher, filter, entities => entities.ForEach(execute));
    }
    public void AddReactEachLate<TEntity>(IMatcher<TEntity> matcher, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReactLate(matcher, filter, entities => entities.ForEach(execute));
    }
    public void AddReactEachLate<TEntity>(TriggerOnEvent<TEntity> trigger, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReactLate(trigger, filter, entities => entities.ForEach(execute));
    }
    public void AddReactEachLate<TEntity>(TriggerOnEvent<TEntity> trigger, Filter filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReactLate(trigger, filter, entities => entities.ForEach(execute));
    }
    public void AddReactEachLate<TEntity>(TriggerOnEvent<TEntity>[] triggers, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReactLate(triggers, filter, entities => entities.ForEach(execute));
    }
    public void AddReactEachLate<TEntity>(ICollector<TEntity> collector, Func<TEntity, bool> filter, Action<TEntity> execute) where TEntity : class, IEntity {
        AddReactLate(collector, filter, entities => entities.ForEach(execute));
    }

    /*

        END LATE REACT

    */





    IContext<TEntity> GetContext<TEntity>() where TEntity : class, IEntity {
        IContext<TEntity> context;
        foreach (var c in contexts.allContexts) {
            context = c as IContext<TEntity>;
            if (context != null) return context;
        }
        throw new ArgumentException("Context was not valid");
    }

    class InlineInitializeSystem : IInitializeSystem {
        Action initialize;

        public InlineInitializeSystem(Action initialize) {
            this.initialize = initialize;
        }

        public void Initialize() {
            initialize();
        }
    }

    class InlineExecuteSystem : IExecuteSystem {
        Action execute;

        public InlineExecuteSystem(Action execute) {
            this.execute = execute;
        }

        public void Execute() {
            execute();
        }
    }

    class InlineCleanupSystem : ICleanupSystem {
        Action cleanup;

        public InlineCleanupSystem(Action cleanup) {
            this.cleanup = cleanup;
        }

        public void Cleanup() {
            cleanup();
        }
    }

    class InlineTeardownSystem : ITearDownSystem {
        Action tearDown;

        public InlineTeardownSystem(Action tearDown) {
            this.tearDown = tearDown;
        }

        public void TearDown() {
            tearDown();
        }
    }
    
    class LateReactiveSystem<TEntity> : InlineReactiveSystem<TEntity> where TEntity : class, IEntity {
        public LateReactiveSystem(ICollector<TEntity> collector, Func<TEntity, bool> filter, Action<List<TEntity>> execute) : base(collector, filter, execute) { }
    }

    class InlineReactiveSystem<TEntity> : ReactiveSystem<TEntity> where TEntity : class, IEntity {
        Func<TEntity, bool> filter;
        Action<List<TEntity>> execute;

        public InlineReactiveSystem(ICollector<TEntity> collector, Func<TEntity, bool> filter, Action<List<TEntity>> execute) : base(collector) {
            this.filter = filter;
            this.execute = execute;
        }

        protected override ICollector<TEntity> GetTrigger(IContext<TEntity> context) {
            throw new Exception("Should never reach here");
        }

        protected override bool Filter(TEntity entity) {
            return filter(entity);
        }

        protected override void Execute(List<TEntity> entities) {
            execute(entities);
        }
    }
}