using Microsoft.Extensions.Logging;

namespace Multi.Data
{
    [LogEventsClass]
    public static class LogEvents
    {
        #region DbContext lifecycle        
        /// <summary>
        /// 100 = Creating a new database context
        /// </summary>
        public static readonly EventId DbCtxCreating = new EventId(100, "Creating a new database context...");
        /// <summary>
        /// 101 = Initializing a new database context
        /// </summary>
        public static readonly EventId DbCtxInitializing = new EventId(101, "Initializing a new database context...");
        /// <summary>
        /// 102 = Database context successfully initialized
        /// </summary>
        public static readonly EventId DbCtxInitSuccess = new EventId(102, "Database context successfully initialized.");
        /// <summary>
        /// -102 = Failed initializing a database context
        /// </summary>
        public static readonly EventId DbCtxInitFailed = new EventId(-102, "Failed initializing a database context.");

        /// <summary>
        /// 110 = Registering a model entity into the database context
        /// </summary>
        public static readonly EventId DbCtxRegisteringModelEntity = new EventId(110, "Registering a model entity into the database context...");
        /// <summary>
        /// 111 = Model entity successfully registered into the database context
        /// </summary>
        public static readonly EventId DbCtxModelEntityRegistrationSuccess = new EventId(111, "Model entity successfully registered into the database context.");
        /// <summary>
        /// -111 = Failed registering a model entity into the database context
        /// </summary>
        public static readonly EventId DbCtxModelEntityRegistrationFailed = new EventId(-111, "Failed registering a model entity into the database context.");
        /// <summary>
        /// 112 = Getting a model entity registration from the database context
        /// </summary>
        public static readonly EventId DbCtxGettingModelEntityRegistration = new EventId(112, "Getting a model entity registration from the database context...");
        /// <summary>
        /// 113 = Model entity registration found in the database context
        /// </summary>
        public static readonly EventId DbCtxGettingModelEntityRegistrationSuccess = new EventId(113, "Model entity registration found in the database context.");
        /// <summary>
        /// -113 = Model entity registration not found in the database context
        /// </summary>
        public static readonly EventId DbCtxModelEntityRegistrationNotFound = new EventId(-113, "Model entity registration not found in the database context.");
        /// <summary>
        /// -114 = Model entity registration type mismatch
        /// </summary>
        public static readonly EventId DbCtxModelEntityRegistrationTypeMismatch = new EventId(-114, "Model entity registration type mismatch.");

        /// <summary>
        /// 190 = IsBusy state of database context has changed
        /// </summary>
        public static readonly EventId DbCtxIsBusyChanged = new EventId(190, "IsBusy state of database context has changed.");
        /// <summary>
        /// 198 = Disposing the database context
        /// </summary>
        public static readonly EventId DbCtxDisposing = new EventId(198, "Disposing the database context...");
        /// <summary>
        /// -198 = Failed disposing the database context
        /// </summary>
        public static readonly EventId DbCtxDisposeFailed = new EventId(-198, "Failed disposing the database context.");
        /// <summary>
        /// 199 = Database context successfully disposed
        /// </summary>
        public static readonly EventId DbCtxDisposeSuccess = new EventId(199, "Database context successfully disposed.");
        #endregion DbContext lifecycle

        #region UnitOrWork lifecycle        
        /// <summary>
        /// 200 = The uow creation
        /// </summary>
        public static readonly EventId UowCreation = new EventId(200);
        /// <summary>
        /// 210 = The uow registration at database CTX
        /// </summary>
        public static readonly EventId UowRegistrationAtDbCtx = new EventId(210);
        /// <summary>
        /// 220 = The ChangeState of the unit of work changed
        /// </summary>
        public static readonly EventId UowChangeStateChanged = new EventId(220);
        /// <summary>
        /// 260 = The uow is starting to commit its changes to the database
        /// </summary>
        public static readonly EventId UowCommiting = new EventId(260);
        /// <summary>
        /// 261 = The uow successfully committed its changes to the database
        /// </summary>
        public static readonly EventId UowCommitSuccess = new EventId(261);
        /// <summary>
        /// -261 = Failed committing uow changes to the database
        /// </summary>
        public static readonly EventId UowCommitFailed = new EventId(-261);
        /// <summary>
        /// 270 = The uow is starting to rolling back its changes from the database
        /// </summary>
        public static readonly EventId UowRollingBack = new EventId(270);
        /// <summary>
        /// 271 = The uow successfully rolled back its changes from the database
        /// </summary>
        public static readonly EventId UowRollbackSuccess = new EventId(271);
        /// <summary>
        /// -271 = Failed rolling back uow changes from the database
        /// </summary>
        public static readonly EventId UowRollbackFailed = new EventId(-271);
        /// <summary>
        /// 280 = The uow removal from database CTX
        /// </summary>
        public static readonly EventId UowRemovalFromDbCtx = new EventId(280);
        /// <summary>
        /// 298 = Disposing the unit of work
        /// </summary>
        public static readonly EventId UowDisposing = new EventId(298, "Disposing the unit of work...");
        /// <summary>
        /// -299 = Failed disposing the unit of work
        /// </summary>
        public static readonly EventId UowDisposeFailed = new EventId(-299, "Failed disposing the unit of work.");
        /// <summary>
        /// 299 = Unit of work successfully disposed
        /// </summary>
        public static readonly EventId UowDisposeSuccess = new EventId(299, "Unit of work successfully disposed.");
        #endregion UnitOrWork lifecycle
    }
}
