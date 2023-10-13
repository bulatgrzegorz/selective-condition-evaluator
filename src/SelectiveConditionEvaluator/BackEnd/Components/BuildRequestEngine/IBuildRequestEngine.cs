﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SelectiveConditionEvaluator.BackEnd.Shared;
using NodeLoggingContext = SelectiveConditionEvaluator.BackEnd.Components.Logging.NodeLoggingContext;

#nullable disable

namespace SelectiveConditionEvaluator.BackEnd.Components.BuildRequestEngine
{
    #region Delegates
    /// <summary>
    /// Callback for event raised when a build request is completed
    /// </summary>
    /// <param name="request">The request which completed</param>
    /// <param name="result">The result for the request</param>
    internal delegate void RequestCompleteDelegate(BuildRequest request, BuildResult result);

    /// <summary>
    /// Callback for event raised when a request is resumed
    /// </summary>
    /// <param name="request">The request being resumed</param>
    internal delegate void RequestResumedDelegate(BuildRequest request);

    /// <summary>
    /// Callback for event raised when a new build request is generated by an MSBuild callback
    /// </summary>
    /// <param name="blocker">Information about what is blocking the engine.</param>
    internal delegate void RequestBlockedDelegate(BuildRequestBlocker blocker);

    /// <summary>
    /// Callback for event raised when the build request engine's status changes.
    /// </summary>
    /// <param name="newStatus">The new status for the engine</param>
    internal delegate void EngineStatusChangedDelegate(BuildRequestEngineStatus newStatus);

    /// <summary>
    /// Callback for event raised when a new configuration needs an ID resolved.
    /// </summary>
    /// <param name="config">The configuration needing an ID</param>
    internal delegate void NewConfigurationRequestDelegate(BuildRequestConfiguration config);

    /// <summary>
    /// Callback for event raised when a resource is requested.
    /// </summary>
    /// <param name="request">The resources being requested</param>
    internal delegate void ResourceRequestDelegate(ResourceRequest request);

    /// <summary>
    /// Callback for event raised when there is an unhandled exception in the engine.
    /// </summary>
    /// <param name="e">The exception.</param>
    internal delegate void EngineExceptionDelegate(Exception e);

    #endregion

    /// <summary>
    /// Status types for the build request engine
    /// </summary>
    internal enum BuildRequestEngineStatus
    {
        /// <summary>
        /// The engine has not yet been initialized, and cannot accept requests.
        /// </summary>
        Uninitialized,

        /// <summary>
        /// The engine has no active or waiting build requests.
        /// </summary>
        Idle,

        /// <summary>
        /// The engine is presently working on a build request.
        /// </summary>
        Active,

        /// <summary>
        /// The engine has only build requests which are waiting for build results to continue.
        /// </summary>
        Waiting,

        /// <summary>
        /// The engine has shut down.
        /// </summary>
        Shutdown
    }

    /// <summary>
    /// Objects implementing this interface may be used by a Node to process build requests
    /// and generate build results.
    /// </summary>
    internal interface IBuildRequestEngine
    {
        #region Events
        /// <summary>
        /// Raised when a build request is completed and results are available.
        /// </summary>
        event RequestCompleteDelegate OnRequestComplete;

        /// <summary>
        /// Raised when a build request is resumed from a previously waiting state.
        /// </summary>
        event RequestResumedDelegate OnRequestResumed;

        /// <summary>
        /// Raised when a new build request is generated by an MSBuild callback.
        /// </summary>
        event RequestBlockedDelegate OnRequestBlocked;

        /// <summary>
        /// Raised when the engine status changes.
        /// </summary>
        event EngineStatusChangedDelegate OnStatusChanged;

        /// <summary>
        /// Raised when a configuration needs an id.
        /// </summary>
        event NewConfigurationRequestDelegate OnNewConfigurationRequest;

        /// <summary>
        /// Raised when resources are requested.
        /// </summary>
        event ResourceRequestDelegate OnResourceRequest;

        /// <summary>
        /// Raised when an unhandled exception occurs in the engine.
        /// </summary>
        event EngineExceptionDelegate OnEngineException;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the current engine status.
        /// </summary>
        BuildRequestEngineStatus Status { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Prepares the engine for a new build and spins up the engine thread.
        /// The engine must be in the Idle state, and not already be initialized.
        /// </summary>
        /// <param name="loggingContext">The logging context for the node.</param>
        void InitializeForBuild(NodeLoggingContext loggingContext);

        /// <summary>
        /// Cleans up after a build but leaves the engine thread running.  Aborts
        /// any outstanding requests.  Blocks until the engine has cleaned up
        /// everything.  After this method is called, InitializeForBuild may be
        /// called to start a new build, or the component may be shut down.
        /// </summary>
        void CleanupForBuild();

        /// <summary>
        /// Submits the specified request to the build queue.
        /// </summary>
        /// <param name="request">The request to build.</param>
        /// <remarks>It is only valid to call this method when the engine is in the Idle or
        /// Waiting state because the engine can only service one active request at a time.</remarks>
        void SubmitBuildRequest(BuildRequest request);

        /// <summary>
        /// Notifies the engine of a build result for a waiting build request.
        /// </summary>
        /// <param name="unblocker">The unblocking information</param>
        void UnblockBuildRequest(BuildRequestUnblocker unblocker);

        /// <summary>
        /// Notifies the engine of a resource response granting the node resources.
        /// </summary>
        /// <param name="response">The resource response.</param>
        void GrantResources(ResourceResponse response);

        /// <summary>
        /// Notifies the engine of a configuration response packet, typically generated by the Build Request Manager.  This packet is used to set
        /// the global configuration ID for a specific configuration.
        /// </summary>
        /// <param name="response">The build configuration response.</param>
        void ReportConfigurationResponse(BuildRequestConfigurationResponse response);

        #endregion
    }
}
