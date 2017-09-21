using Readme.Common.Enum;
using Readme.Logic.DomainModel;
using Readme.Logic.DomainModel.LineModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readme.Logic.V1.Interface
{
    public interface ILineService
    {

        Task<byte[]> GetUserUploadedContent(string ContentID, string ChannelAccessToken);
        Task<LineUserInfoDto> GetUserInfo(string UID, string ChannelAccessToken);
        Task<string> PushMessage(string ToUserID, string Message, string ChannelAccessToken);
        Task<string> PushDynamicMessage(string ToUserID, dynamic Message, string ChannelAccessToken);
        Task ReplyMessage(string JasonData, string ChannelAccessToken);

        dynamic DeclareDynamicAreasModel(AreaDto Areas);
        dynamic DeclareDynamicActionsModel(ActionDto Actions);
        dynamic DeclareDynamicColumnModel(ActionDto[] Actions, string ThumbnailImageUrl, string Title, string Text);
        dynamic DeclareDynamicCarouselTemplateModel(ColumnDto[] Columns, string AltText);
        dynamic DeclareDynamicConfirmTemplateModel(ActionDto[] Actions, string AltText, string Text);
        dynamic DeclareDynamicTemplateModel(ActionDto[] Actions, string AltText, string Text, string Title, string ThumbnailImageUrl);
        dynamic DeclareDynamicTemplateButtonModel(ActionDto[] Actions, string AltText, string Text, string Title);
        dynamic DeclareDynamicVideoModel(string OriginalContentUrl, string PreviewImageUrl);
        dynamic DeclareDynamicLocationModel(string Title, string Address, double Latitude, double Longitude);
        dynamic DeclareDynamicStickernModel(int PackageId, int StickerId);
        dynamic DeclareDynamicImagemapModel(ActionDto[] Actions, string BaseUrl, string AltText);

        Task<string> SendDynamicMessageToCustommer(ReceievedMessageDto ReceivedMessage);

        //List<LogUserModel> TakeUserListByProjectId(int ProjectId);
        //List<LogMessageModel> TakeLog(string postData);
        //void UpdateAvailableStatus(string JsonObj);

        //void PrepairLineSaveLog(string channelAccessToken, string sourceUID, string destinationUID, string Serialize, string typeUserSource, Guid? fromUserMemberGuId);
        //void logLineMessage(EventDto sourceEventModel, string channelAccessToken, string destination);
        //bool CheckAvailableStatusBy_uid(string uid, ActiveStatusLineLogEnum availableStatus);
    }
}
