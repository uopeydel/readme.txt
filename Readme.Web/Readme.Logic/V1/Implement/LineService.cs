using Readme.DataAccess.EntityFramework.UnitOfWork.Interface;
using Readme.Logic.DomainModel;
using Readme.Logic.V1.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Readme.Logic.DomainModel.LineModels;
using Readme.Common.Enum;
using System.Net.Http;
using System.Dynamic;

namespace Readme.Logic.V1.Implement
{
    public class LineService : ILineService
    {
        public async Task<byte[]> GetUserUploadedContent(string ContentID, string ChannelAccessToken)
        {
            try
            {
                ContentID = ContentID.Trim();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
                return await client.GetByteArrayAsync("https://api.line.me/v2/bot/message/" + ContentID + "/content");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LineUserInfoDto> GetUserInfo(string UID, string ChannelAccessToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
                var data = await client.GetByteArrayAsync("https://api.line.me/v2/bot/profile/" + UID);
                return JsonConvert.DeserializeObject<LineUserInfoDto>(Encoding.UTF8.GetString(data));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> PushMessage(string ToUserID, string Message, string ChannelAccessToken)
        {
            try
            {
                dynamic JsonObj = new ExpandoObject();
                JsonObj.to = ToUserID;
                JsonObj.messages = new List<ExpandoObject>();
                dynamic JsonObj_Message = new ExpandoObject();
                JsonObj_Message.type = "text";
                JsonObj_Message.text = Message;
                JsonObj.messages.Add(JsonObj_Message);
                string JsonObjSerialize = JsonConvert.SerializeObject(JsonObj);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
                StringContent stringContent = new StringContent(JsonObjSerialize, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.line.me/v2/bot/message/push", stringContent);
                response.EnsureSuccessStatusCode();

                return JsonConvert.SerializeObject(JsonObj.messages);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message">Is Json Dto Message</param>
        /// <param name="ChannelAccessToken"></param>
        /// <returns></returns>
        public async Task<string> PushDynamicMessage(string ToUserID, dynamic Message, string ChannelAccessToken)
        {
            try
            {
                dynamic JsonObj = new ExpandoObject();
                JsonObj.to = ToUserID;
                JsonObj.messages = new List<ExpandoObject>();
                JsonObj.messages.Add(Message);
                var Serialize = JsonConvert.SerializeObject(JsonObj);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
                StringContent stringContent = new StringContent(Serialize, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.line.me/v2/bot/message/push", stringContent);
                response.EnsureSuccessStatusCode();

                return JsonConvert.SerializeObject(JsonObj.messages);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task ReplyMessage(string JasonData, string ChannelAccessToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
                StringContent stringContent = new StringContent(JasonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.line.me/v2/bot/message/reply", stringContent);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic DeclareDynamicAreasModel(AreaDto Areas)
        {
            dynamic area = new ExpandoObject();
            area.x = Areas.x;// 28;
            area.y = Areas.y;// 438;
            area.width = Areas.width;// 165;
            area.height = Areas.height; // 155;

            return area;
        }

        public dynamic DeclareDynamicActionsModel(ActionDto Action)
        {
            dynamic actionModel = new ExpandoObject();
            if (Action.area == null)
            {
                if (Action.type == "postback")
                {
                    actionModel.type = Action.type;// "postback";
                    actionModel.label = Action.label;
                    actionModel.data = Action.data; // "action=buy&itemid=111";
                }
                else if (Action.type == "uri")
                {
                    actionModel.type = Action.type;//"uri";
                    actionModel.label = Action.label;
                    actionModel.uri = Action.uri; //"https://www.facebook.com/";
                }
                else if (Action.type == "message")
                {
                    actionModel.type = Action.type;//"message";
                    actionModel.label = Action.label;
                    actionModel.text = Action.text; //"yes";
                }
            }
            else
            {
                if (Action.type == "uri")
                {
                    actionModel.type = Action.type;//"uri";
                    actionModel.linkUri = Action.linkUri; //link uri   
                    actionModel.area = DeclareDynamicAreasModel(Action.area);
                }
                else if (Action.type == "message")
                {
                    actionModel.type = Action.type; // "message";
                    actionModel.text = Action.text; //text
                    actionModel.area = DeclareDynamicAreasModel(Action.area);
                }
            }
            return actionModel;
        }

        public dynamic DeclareDynamicColumnModel(ActionDto[] Actions, string ThumbnailImageUrl, string Title, string Text)
        {
            dynamic columns = new ExpandoObject();
            columns.thumbnailImageUrl = ThumbnailImageUrl; // link https png jpg
            columns.title = Title; // "this is menu";
            columns.text = Text;// "description";
            columns.actions = new List<ExpandoObject>();
            foreach (ActionDto detailAction in Actions)
            {
                columns.actions.Add(DeclareDynamicActionsModel(detailAction));
            }

            return columns;
        }

        public dynamic DeclareDynamicCarouselTemplateModel(ColumnDto[] Columns, string AltText)
        {
            dynamic templateSub = new ExpandoObject();
            templateSub.type = "carousel";
            templateSub.columns = new List<ExpandoObject>();
            foreach (ColumnDto column in Columns)
            {
                templateSub.columns.Add(DeclareDynamicColumnModel(column.actions, column.thumbnailImageUrl, column.title, column.text));
            }

            dynamic template = new ExpandoObject();
            template.type = "template";
            template.altText = AltText; // "this is a carousel template";
            template.template = templateSub;// new List<ExpandoObject>();

            return template;
        }

        public dynamic DeclareDynamicConfirmTemplateModel(ActionDto[] Actions, string AltText, string Text)
        {
            dynamic templateSub = new ExpandoObject();
            templateSub.type = "confirm";
            templateSub.text = Text; //"Are you sure?";
            templateSub.actions = new List<ExpandoObject>();
            foreach (ActionDto detailAction in Actions)
            {
                templateSub.actions.Add(DeclareDynamicActionsModel(detailAction));
            }

            dynamic template = new ExpandoObject();
            template.type = "template";
            template.altText = AltText;
            template.template = templateSub;// new List<ExpandoObject>();

            return template;
        }

        public dynamic DeclareDynamicTemplateModel(ActionDto[] Actions, string AltText, string Text, string Title, string ThumbnailImageUrl)
        {
            dynamic templateSub = new ExpandoObject();
            templateSub.type = "buttons";
            templateSub.thumbnailImageUrl = ThumbnailImageUrl; //link https png jpg
            templateSub.title = Title; // "Menu";
            templateSub.text = Text; // "Please select";
            templateSub.actions = new List<ExpandoObject>();
            foreach (ActionDto detailAction in Actions)
            {
                templateSub.actions.Add(DeclareDynamicActionsModel(detailAction));
            }

            dynamic template = new ExpandoObject();
            template.type = "template";
            template.altText = AltText; // "this is a buttons template";
            template.template = templateSub;// new List<ExpandoObject>(); 
            return template;
        }

        public dynamic DeclareDynamicTemplateButtonModel(ActionDto[] Actions, string AltText, string Text, string Title)
        {
            dynamic templateSub = new ExpandoObject();
            templateSub.type = "buttons";
            templateSub.title = Title; // "Menu";
            templateSub.text = Text; // "Please select";
            templateSub.actions = new List<ExpandoObject>();
            foreach (ActionDto detailAction in Actions)
            {
                templateSub.actions.Add(DeclareDynamicActionsModel(detailAction));
            }

            dynamic template = new ExpandoObject();
            template.type = "template";
            template.altText = AltText; // "this is a buttons template";
            template.template = templateSub;// new List<ExpandoObject>(); 
            return template;
        }

        public dynamic DeclareDynamicTextModel(string Message)
        {
            dynamic text = new ExpandoObject();
            text.type = "text";
            text.text = Message;
            return text;
        }

        public dynamic DeclareDynamicImageModel(string OriginalContentUrl, string PreviewImageUrl)
        {
            dynamic image = new ExpandoObject();
            image.type = "image";
            image.originalContentUrl = OriginalContentUrl; // link https png jpg
            image.previewImageUrl = PreviewImageUrl; // link https png jpg

            return image;
        }

        public dynamic DeclareDynamicVideoModel(string OriginalContentUrl, string PreviewImageUrl)
        {
            dynamic video = new ExpandoObject();
            video.type = "video";
            video.originalContentUrl = OriginalContentUrl; // link https mp4
            video.previewImageUrl = PreviewImageUrl; // prev image jpg png

            return video;
        }

        public dynamic DeclareDynamicAudioModel(string OriginalContentUrl, int Duration)
        {
            dynamic audio = new ExpandoObject();
            audio.type = "audio";
            audio.originalContentUrl = OriginalContentUrl; // link https m4a
            audio.duration = Duration; // long time

            return audio;
        }

        public dynamic DeclareDynamicLocationModel(string Title, string Address, double Latitude, double Longitude)
        {
            dynamic location = new ExpandoObject();
            location.type = "location";
            location.title = Title; // "Longkong studio";
            location.address = Address; // "Longkong studio Address";
            location.latitude = Latitude; // 13.891185;
            location.longitude = Longitude; // 100.559556;

            return location;
        }

        public dynamic DeclareDynamicStickernModel(int PackageId, int StickerId)
        {
            dynamic sticker = new ExpandoObject();
            sticker.type = "sticker";
            sticker.packageId = PackageId;//// 1,2,3;
            sticker.stickerId = StickerId; //  rnd.Next(2, 15);

            return sticker;
        }

        public dynamic DeclareDynamicImagemapModel(ActionDto[] Actions, string BaseUrl, string AltText)
        {
            dynamic baseSized = new ExpandoObject();
            baseSized.height = 1040;
            baseSized.width = 1040;

            dynamic Imagemap = new ExpandoObject();
            Imagemap.type = "imagemap";
            Imagemap.baseUrl = BaseUrl;// "https://raw.githubusercontent.com/uopeydel/testline1/master/imagefolder/picstar"; image folder root
            Imagemap.altText = AltText; // "this is an imagemap";
            Imagemap.baseSize = baseSized;

            Imagemap.actions = new List<ExpandoObject>();
            foreach (ActionDto detailAction in Actions)
            {
                Imagemap.actions.Add(DeclareDynamicActionsModel(detailAction));
            }
            return Imagemap;
        }

        public async Task<string> SendDynamicMessageToCustommer(ReceievedMessageDto ReceivedMessage)
        {
            try
            {
                dynamic DataJson = new ExpandoObject();

                if (ReceivedMessage.events[0].message.type == "text")
                {
                    DataJson = DeclareDynamicTextModel(ReceivedMessage.events[0].message.text);
                }
                else if (ReceivedMessage.events[0].message.type == "image")
                {
                    DataJson = DeclareDynamicImageModel(
                         ReceivedMessage.events[0].message.originalContentUrl,
                        ReceivedMessage.events[0].message.previewImageUrl

                        );
                }
                else if (ReceivedMessage.events[0].message.type == "video")
                {
                    DataJson = DeclareDynamicVideoModel(
                        ReceivedMessage.events[0].message.originalContentUrl,
                        ReceivedMessage.events[0].message.previewImageUrl

                        );
                }
                else if (ReceivedMessage.events[0].message.type == "audio")
                {
                    DataJson = DeclareDynamicAudioModel(
                        ReceivedMessage.events[0].message.originalContentUrl,
                        ReceivedMessage.events[0].message.duration
                        );
                }
                else if (ReceivedMessage.events[0].message.type == "location")
                {
                    DataJson = DeclareDynamicLocationModel(
                        ReceivedMessage.events[0].message.title,
                        ReceivedMessage.events[0].message.address,
                        ReceivedMessage.events[0].message.latitude,
                        ReceivedMessage.events[0].message.longitude
                        );
                }
                else if (ReceivedMessage.events[0].message.type == "sticker")
                {
                    DataJson = DeclareDynamicStickernModel(ReceivedMessage.events[0].message.packageId, ReceivedMessage.events[0].message.stickerId);
                }
                else if (ReceivedMessage.events[0].message.type == "imagemap")
                {
                    DataJson = DeclareDynamicImagemapModel(
                        ReceivedMessage.events[0].message.actions,
                        ReceivedMessage.events[0].message.baseUrl,
                        ReceivedMessage.events[0].message.altText

                        );
                }
                else if ((ReceivedMessage.events[0].message.type == "buttons" || ReceivedMessage.events[0].message.type == "template") && ReceivedMessage.events[0].message.template.type == "buttons") //template
                {
                    if (string.IsNullOrEmpty(ReceivedMessage.events[0].message.template.thumbnailImageUrl))
                    {
                        DataJson = DeclareDynamicTemplateButtonModel(
                         ReceivedMessage.events[0].message.template.actions,
                         ReceivedMessage.events[0].message.altText,
                         ReceivedMessage.events[0].message.template.text,
                         ReceivedMessage.events[0].message.template.title
                         );
                    }
                    else
                    {
                        DataJson = DeclareDynamicTemplateModel(
                        ReceivedMessage.events[0].message.template.actions,
                        ReceivedMessage.events[0].message.altText,
                        ReceivedMessage.events[0].message.template.text,
                        ReceivedMessage.events[0].message.template.title,
                        ReceivedMessage.events[0].message.template.thumbnailImageUrl
                        );
                    }

                }
                else if (ReceivedMessage.events[0].message.type == "buttons" && ReceivedMessage.events[0].message.template.type == "carousel")//carouseltemplate
                {
                    DataJson = DeclareDynamicCarouselTemplateModel(
                        ReceivedMessage.events[0].message.template.columns,
                        ReceivedMessage.events[0].message.altText);
                }
                else if (ReceivedMessage.events[0].message.type == "buttons" && ReceivedMessage.events[0].message.template.type == "confirm")//confirm
                {
                    DataJson = DeclareDynamicConfirmTemplateModel(
                        ReceivedMessage.events[0].message.template.actions,
                        ReceivedMessage.events[0].message.altText,
                        ReceivedMessage.events[0].message.text);
                }

                var ChannelAccessToken = ReceivedMessage.events[0].ChannelAccessToken;//ProjectHooklineTokensRefSrv.TakeProjectHooklineTokensBy_ProjectHookParams(projHook).ChannelAccessToken;
                string MessageJson = await PushDynamicMessage(ReceivedMessage.events[0].to, DataJson, ChannelAccessToken);
                //PrepairLineSaveLog(
                //    ChannelAccessToken,
                //    ReceivedMessage.events[0].source.userId,
                //    ReceivedMessage.events[0].to,
                //    MessageJson,
                //    "developer",
                //    ReceivedMessage.events[0].fromUserMemberGuId);
                return MessageJson;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


    }
}
