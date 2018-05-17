using MRS.Domain.States;

namespace MRS.Domain.Utils
{
    public static class RequestExtenstions
    {
        public static RequestState New(this Request request)
        {
            return new NewState(request);
        }

        public static RequestState Approved(this Request request)
        {
            return new ApprovedState(request);
        }

        public static RequestState Rejected(this Request request)
        {
            return new RejectedState(request);
        }

        public static RequestState WorkAssigned(this Request request)
        {
            return new WorkAssignedState(request);
        }

        public static RequestState WorkStarted(this Request request)
        {
            return new WorkStartedState(request);
        }

        public static RequestState WorkRejected(this Request request)
        {
            return new WorkRejectedState(request);
        }

        public static RequestState Completed(this Request request)
        {
            return new CompletedState(request);
        }
    }
}
