using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi.Data
{
    /// <summary>
    /// Logging extensions for basic data handling events
    /// </summary>
    public static class LogExtensions
    {
        #region DbCtx        
        /// <summary>
        /// Logs the creation of a new database context.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        public static void LogDbCtxCreation(this ILogger logger, LogLevel level)
        {
            logger?.Log(level, LogEvents.DbCtxCreating, "Database context is being created...");
        }

        /// <summary>
        /// Logs the start of disposing of a database context.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        public static void LogDbCtxDisposing(this ILogger logger, LogLevel level)
        {
            logger?.Log(level, LogEvents.DbCtxDisposing, "Database context is being disposed...");
        }

        /// <summary>
        /// Logs the success of disposing a database context instance.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        public static void LogDbCtxDisposeSuccess(this ILogger logger, LogLevel level)
        {
            logger?.Log(level, LogEvents.DbCtxDisposeSuccess, "Database context successfully disposed.");
        }

        /// <summary>
        /// Logs the failure of disposing a database context instance.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="ex">The exception causing the dispose to fail.</param>
        public static void LogDbCtxDisposeFailed(this ILogger logger, LogLevel level, Exception ex)
        {
            logger?.Log(level, LogEvents.DbCtxDisposeFailed, "Disposing database context failed. Exception: {exception}", ex);
        }

        /// <summary>
        /// Logs the change of the IsBusy state of the database context.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="isBusy">if set to <c>true</c> [is busy].</param>
        public static void LogDbCtxIsBusyChanged(this ILogger logger, LogLevel level, bool isBusy)
        {
            if (isBusy)
                logger?.Log(level, LogEvents.DbCtxIsBusyChanged, "Database context is busy.");
            else
                logger?.Log(level, LogEvents.DbCtxIsBusyChanged, "Database context is idle.");
        }
        #endregion DbCtx

        #region Uow        
        /// <summary>
        /// Logs the creation of a new unit of work.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowCreation(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowCreation, "Unit of work created with {uow.Id}.", uowId);
        }

        /// <summary>
        /// Logs the unit of work registration at the database context.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowRegistrationAtDbCtx(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowRegistrationAtDbCtx, "Unit of work with {uow.Id} registered at database context.", uowId);
        }

        /// <summary>
        /// Logs the change of the ChangeState of the unit of work.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        /// <param name="from">The previous change state.</param>
        /// <param name="to">The new change state.</param>
        public static void LogUowChangeStateChanged(this ILogger logger, LogLevel level, int uowId, UowChangeStateEnum from, UowChangeStateEnum to)
        {
            logger?.Log(level, LogEvents.UowChangeStateChanged, "Change state of unit of work with {uow.Id} changed from {oldValue} to {newValue}.", uowId, from, to);
        }
        

        /// <summary>
        /// Logs the unit of work removal from the database context.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowRemovalFromDbCtx(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowRemovalFromDbCtx, "Unit of work with {uow.Id} removed from database context.", uowId);
        }

        /// <summary>
        /// Logs the start of uow committing changes to the database.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowCommiting(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowCommiting, "Unit of work {uow.Id} commiting changes to the database...", uowId);
        }

        /// <summary>
        /// Logs the success of uow committing changes to the database.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        /// <param name="duration">The time it took to commit changes.</param>
        public static void LogUowCommitSuccess(this ILogger logger, LogLevel level, int uowId, TimeSpan duration)
        {
            logger?.Log(level, LogEvents.UowCommitSuccess, "Unit of work {uow.Id} successfully committed changes to the database in {durationMillisec} milliseconds.", uowId, duration.Milliseconds);
        }

        /// <summary>
        /// Logs the failure of uow committing changes to the database.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        /// <param name="duration">The time commit took before catching the error.</param>
        /// <param name="ex">The exception causing the commit to fail.</param>
        public static void LogUowCommitFailed(this ILogger logger, LogLevel level, int uowId, TimeSpan duration, Exception ex)
        {
            logger?.Log(level, LogEvents.UowCommitFailed, "Unit of work {uow.Id} failed to commit changes to the database after {durationMillisec} milliseconds. Exception: {exception}", uowId, duration, ex);
        }

        /// <summary>
        /// Logs the start of uow rolling back changes from the database.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowRollingBack(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowRollingBack, "Unit of work {uow.Id} rolling back changes from the database...", uowId);
        }

        /// <summary>
        /// Logs the success of uow rolling back changes from the database.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        /// <param name="duration">The time it took to roll back changes.</param>
        public static void LogUowRollbackSuccess(this ILogger logger, LogLevel level, int uowId, TimeSpan duration)
        {
            logger?.Log(level, LogEvents.UowRollbackSuccess, "Unit of work {uow.Id} successfully rolled back changes from the database in {durationMillisec} milliseconds.", uowId, duration.Milliseconds);
        }

        /// <summary>
        /// Logs the failure of uow rolling back changes from the database.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        /// <param name="duration">The time rollback took before catching the error.</param>
        /// <param name="ex">The exception causing the rollback to fail.</param>
        public static void LogUowRollbackFailed(this ILogger logger, LogLevel level, int uowId, TimeSpan duration, Exception ex)
        {
            logger?.Log(level, LogEvents.UowRollbackFailed, "Unit of work {uow.Id} failed to roll back changes from the database after {durationMillisec} milliseconds. Exception: {exception}", uowId, duration, ex);
        }

        /// <summary>
        /// Logs the start of disposing of a unit of work.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowDisposing(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowDisposing, "Unit of work with {uow.Id} is being disposed...", uowId);
        }

        /// <summary>
        /// Logs the success of disposing a unit of work instance.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        public static void LogUowDisposeSuccess(this ILogger logger, LogLevel level, int uowId)
        {
            logger?.Log(level, LogEvents.UowDisposeSuccess, "Unit of work successfully disposed.");
        }

        /// <summary>
        /// Logs the failure of disposing a unit of work instance.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="level">The level.</param>
        /// <param name="uowId">The uow identifier.</param>
        /// <param name="ex">The exception causing the dispose to fail.</param>
        public static void LogUowDisposeFailed(this ILogger logger, LogLevel level, int uowId, Exception ex)
        {
            logger?.Log(level, LogEvents.UowDisposeFailed, "Disposing unit of work failed. Exception: {exception}", ex);
        }
        #endregion Uow
    }
}
