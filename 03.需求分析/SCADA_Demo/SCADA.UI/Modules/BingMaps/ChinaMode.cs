using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.Core;
using Microsoft.Maps.MapControl;

namespace SCADA.UI.Modules.BingMaps
{
    /// <summary>
    /// 自定义地图投影模式
    /// </summary>
    public class ChinaMode : MercatorMode
    {
        // 地图坐标取值范围最小（-85.05112877980659,-180）最大（85.05112877980659,180）
        // 中国范围大致是：LocationRect(new Location(60, 60), new Location(13, 140))

        ///　<summary>
        ///　纬度范围
        ///　</summary>
        public Range<double> LatitudeRange = new Range<double>(-85.05112877980659, -180);
        ///　<summary>
        ///　经度范围
        ///　</summary>
        public Range<double> LongitudeRange = new Range<double>(85.05112877980659, 180);
        ///　<summary>
        ///　深度缩放范围
        ///　</summary>
        public Range<double> MapZoomRange = new Range<double>(1.0, 20.0);

        /// <summary>
        /// 获取中文地图图层
        /// </summary>
        public MapTileLayer GetChinaTileLayer()
        {
            //中文地图瓦片
            MapTileLayer tileChinaLayer = new MapTileLayer();
            //初始化一个Uri对象，指向中文必应地图的Tile系统
            UriBuilder tileSourceUri = new UriBuilder("http://r2.tiles.ditu.live.com/tiles/r{quadkey}.png?g=41");
            LocationRectTileSource tileSource = new LocationRectTileSource(
            tileSourceUri.Uri.ToString(), new LocationRect(new Location(LatitudeRange.From, LatitudeRange.To), new Location(LongitudeRange.From, LongitudeRange.To)),
             new Range<double>(MapZoomRange.From, MapZoomRange.To));
            tileChinaLayer.TileSources.Add(tileSource); //指定图层的TileSource
            tileChinaLayer.Opacity = 1;
            return tileChinaLayer;
        }

        ///　<summary>
        ///　重写方法
        ///　</summary>
        ///　<param　name="center">地图中心地理坐标</param>
        ///　<param　name="zoomLevel">地图缩放级别</param>
        ///　<param　name="heading">定向视图标题</param>
        ///　<param　name="pitch"></param>
        ///　<returns>是否有值更改</returns>
        public override bool ConstrainView(Location center, ref　double zoomLevel, ref　double heading, ref　double pitch)
        {
            return base.ConstrainView(center, ref　zoomLevel, ref　heading, ref　pitch);
        }
        ///　<summary>
        ///　确定地图深度缩放范围
        ///　</summary>
        ///　<param　name="center"></param>
        ///　<returns></returns>
        protected override Range<double> GetZoomRange(Location center)
        {
            //只能访问3-10级地图
            return this.MapZoomRange;
        }
        ///　<summary>
        ///　重写鼠标中键滚轮操作行为
        ///　</summary>
        ///　<param　name="e"></param>
        public override void OnMouseWheel(MapMouseWheelEventArgs e)
        {
            if ((e.WheelDelta > 0.0) && (this.ZoomLevel >= this.MapZoomRange.To))
            {
                e.Handled = true;
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }
        ///　<summary>
        ///　重写鼠标拖放地图行为
        ///　</summary>
        ///　<param　name="e"></param>
        public override void OnMouseDragBox(MapMouseDragEventArgs e)
        {
            if (((this.TargetBoundingRectangle.East <= this.LongitudeRange.To)
                && (this.TargetBoundingRectangle.West >= this.LongitudeRange.From)))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
