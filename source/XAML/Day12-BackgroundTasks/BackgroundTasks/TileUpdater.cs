using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BackgroundTasks
{
    public sealed class TileUpdater : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            XmlDocument tileData = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText04);
            XmlNodeList textData = tileData.GetElementsByTagName("text");
            textData[0].InnerText = "Background updates are absolutely amazing. #31daysofwin8";
            TileNotification notification = new TileNotification(tileData);
            notification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }
    }
}
