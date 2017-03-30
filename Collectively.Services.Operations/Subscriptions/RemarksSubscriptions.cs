using Collectively.Messages.Commands.Remarks;
using Collectively.Messages.Events.Remarks;
using static Collectively.Common.Host.WebServiceHost;

namespace Collectively.Services.Operations.Subscriptions
{
    public static class RemarksSubscriptions
    {
        public static BusBuilder SubscribeRemarks(this BusBuilder busBuilder)
            => busBuilder.SubscribeCommands()
                         .SubscribeEvents();

        private static BusBuilder SubscribeCommands(this BusBuilder busBuilder)
            => busBuilder.SubscribeToCommand<CreateRemark>()
                .SubscribeToCommand<DeleteRemark>()
                .SubscribeToCommand<ResolveRemark>()
                .SubscribeToCommand<ProcessRemark>()
                .SubscribeToCommand<RenewRemark>()
                .SubscribeToCommand<CancelRemark>()
                .SubscribeToCommand<AddPhotosToRemark>()
                .SubscribeToCommand<RemovePhotosFromRemark>()
                .SubscribeToCommand<SubmitRemarkVote>()
                .SubscribeToCommand<DeleteRemarkVote>()
                .SubscribeToCommand<AddCommentToRemark>()
                .SubscribeToCommand<EditRemarkComment>()
                .SubscribeToCommand<DeleteRemarkComment>()
                .SubscribeToCommand<SubmitRemarkCommentVote>()
                .SubscribeToCommand<DeleteRemarkCommentVote>()
                .SubscribeToCommand<AddFavoriteRemark>()
                .SubscribeToCommand<DeleteFavoriteRemark>();                                

        private static BusBuilder SubscribeEvents(this BusBuilder busBuilder)
            => busBuilder.SubscribeToEvent<RemarkCreated>()
                .SubscribeToEvent<CreateRemarkRejected>()
                .SubscribeToEvent<RemarkDeleted>()
                .SubscribeToEvent<DeleteRemarkRejected>()
                .SubscribeToEvent<RemarkResolved>()
                .SubscribeToEvent<ResolveRemarkRejected>()
                .SubscribeToEvent<RemarkProcessed>()
                .SubscribeToEvent<ProcessRemarkRejected>()
                .SubscribeToEvent<RemarkRenewed>()
                .SubscribeToEvent<RenewRemarkRejected>()
                .SubscribeToEvent<RemarkCanceled>()
                .SubscribeToEvent<CancelRemarkRejected>()                                                
                .SubscribeToEvent<PhotosToRemarkAdded>()
                .SubscribeToEvent<AddPhotosToRemarkRejected>()
                .SubscribeToEvent<PhotosFromRemarkRemoved>()
                .SubscribeToEvent<RemovePhotosFromRemarkRejected>()  
                .SubscribeToEvent<RemarkVoteSubmitted>()
                .SubscribeToEvent<SubmitRemarkVoteRejected>()
                .SubscribeToEvent<RemarkVoteDeleted>()
                .SubscribeToEvent<DeleteRemarkVoteRejected>()
                .SubscribeToEvent<CommentAddedToRemark>()                
                .SubscribeToEvent<AddCommentToRemarkRejected>()
                .SubscribeToEvent<CommentEditedInRemark>()                
                .SubscribeToEvent<EditRemarkCommentRejected>()
                .SubscribeToEvent<CommentDeletedFromRemark>()                
                .SubscribeToEvent<DeleteRemarkCommentRejected>()
                .SubscribeToEvent<RemarkCommentVoteSubmitted>()                
                .SubscribeToEvent<SubmitRemarkCommentVoteRejected>()
                .SubscribeToEvent<RemarkCommentVoteDeleted>()
                .SubscribeToEvent<DeleteRemarkCommentVoteRejected>()
                .SubscribeToEvent<FavoriteRemarkAdded>()                
                .SubscribeToEvent<AddFavoriteRemarkRejected>()
                .SubscribeToEvent<FavoriteRemarkDeleted>()
                .SubscribeToEvent<DeleteFavoriteRemarkRejected>();                
    }
}