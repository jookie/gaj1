using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site2App.WP8.IconEnum
{
    public class IconEnumHelper
    {
        Dictionary<string, string> symbolEnums = new Dictionary<string, string>();
        public IconEnumHelper()
        
        {
            ///////////////////////////////////////////
            // missing "manage"
            //symbolEnums.Add("manage","\uE175");

            symbolEnums.Add("previous", "\uE100");
            symbolEnums.Add("next", "\uE101");
            symbolEnums.Add("play", "\uE102");
            symbolEnums.Add("pause", "\uE103");
            symbolEnums.Add("edit", "\uE104");
            symbolEnums.Add("save", "\uE105");
            symbolEnums.Add("clear", "\uE106");
            symbolEnums.Add("delete", "\uE107");
            symbolEnums.Add("remove", "\uE108");
            symbolEnums.Add("add", "\uE109");
            symbolEnums.Add("cancel", "\uE10A");
            symbolEnums.Add("accept", "\uE10B");
            symbolEnums.Add("more", "\uE10C");
            symbolEnums.Add("redo", "\uE10D");
            symbolEnums.Add("undo", "\uE10E");
            symbolEnums.Add("home", "\uE10F");

            symbolEnums.Add("up", "\uE110");
            symbolEnums.Add("forward", "\uE111");
            symbolEnums.Add("right", "\uE111");
            symbolEnums.Add("back", "\uE112");
            symbolEnums.Add("left", "\uE112");
            symbolEnums.Add("favorite", "\uE113");
            symbolEnums.Add("camera", "\uE114");
            symbolEnums.Add("settings", "\uE115");
            symbolEnums.Add("video", "\uE116");
            symbolEnums.Add("sync", "\uE117");
            symbolEnums.Add("download", "\uE118");
            symbolEnums.Add("mail", "\uE119");
            symbolEnums.Add("find", "\uE11A");
            symbolEnums.Add("help", "\uE11B");
            symbolEnums.Add("upload", "\uE11C");
            symbolEnums.Add("emoji", "\uE11D");
            symbolEnums.Add("twopage", "\uE11E");
            symbolEnums.Add("leavechat", "\uE11F");

            symbolEnums.Add("mailforward", "\uE120");
            symbolEnums.Add("clock", "\uE121");
            symbolEnums.Add("send", "\uE122");
            symbolEnums.Add("crop", "\uE123");
            symbolEnums.Add("rotatecamera", "\uE124");
            symbolEnums.Add("people", "\uE125");
            symbolEnums.Add("closepane", "\uE126");
            symbolEnums.Add("openpane", "\uE127");
            symbolEnums.Add("world", "\uE128");
            symbolEnums.Add("flag", "\uE129");
            symbolEnums.Add("previewlink", "\uE12A");
            symbolEnums.Add("globe", "\uE12B");
            symbolEnums.Add("trim", "\uE12C");
            symbolEnums.Add("attachcamera", "\uE12D");
            symbolEnums.Add("zoomin", "\uE12E");
            symbolEnums.Add("bookmarks", "\uE12F");

            symbolEnums.Add("document", "\uE130");
            symbolEnums.Add("protecteddocument", "\uE131");
            symbolEnums.Add("page", "\uE132");
            symbolEnums.Add("bullets", "\uE133");
            symbolEnums.Add("comment", "\uE134");
            symbolEnums.Add("mail2", "\uE135");
            symbolEnums.Add("contactinfo", "\uE136");
            symbolEnums.Add("hangup", "\uE137");
            symbolEnums.Add("viewall", "\uE138");
            symbolEnums.Add("mappin", "\uE139");
            symbolEnums.Add("phone", "\uE13A");
            symbolEnums.Add("videochat", "\uE13B");
            symbolEnums.Add("switch", "\uE13C");
            symbolEnums.Add("contact", "\uE13D");
            symbolEnums.Add("rename", "\uE13E");
            symbolEnums.Add("pin", "\uE141");
            symbolEnums.Add("musicinfo", "\uE142");
            symbolEnums.Add("go", "\uE143");
            symbolEnums.Add("keyboard", "\uE144");
            symbolEnums.Add("dockleft", "\uE145");
            symbolEnums.Add("dockright", "\uE146");
            symbolEnums.Add("dockbottom", "\uE147");
            symbolEnums.Add("remote", "\uE148");
            symbolEnums.Add("refresh", "\uE149");
            symbolEnums.Add("rotate", "\uE14A");
            symbolEnums.Add("shuffle", "\uE14B");
            symbolEnums.Add("list", "\uE14C");
            symbolEnums.Add("shop", "\uE14D");
            symbolEnums.Add("selectall", "\uE14E");
            symbolEnums.Add("orientation", "\uE14F");

            symbolEnums.Add("import", "\uE150");
            symbolEnums.Add("importall", "\uE151");
            symbolEnums.Add("browsephotos", "\uE155");
            symbolEnums.Add("webcam", "\uE156");
            symbolEnums.Add("pictures", "\uE158");
            symbolEnums.Add("savelocal", "\uE159");
            symbolEnums.Add("caption", "\uE15A");
            symbolEnums.Add("stop", "\uE15B");
            symbolEnums.Add("showresults", "\uE15C");
            symbolEnums.Add("volume", "\uE15D");
            symbolEnums.Add("repair", "\uE15E");
            symbolEnums.Add("message", "\uE15F");
            symbolEnums.Add("page2", "\uE160");
            symbolEnums.Add("calendarday", "\uE161");
            symbolEnums.Add("calendarweek", "\uE162");
            symbolEnums.Add("calendar", "\uE163");
            symbolEnums.Add("characters", "\uE164");
            symbolEnums.Add("mailreplyall", "\uE165");
            symbolEnums.Add("read", "\uE166");
            symbolEnums.Add("link", "\uE167");
            symbolEnums.Add("accounts", "\uE168");
            symbolEnums.Add("showbcc", "\uE169");
            symbolEnums.Add("hidebcc", "\uE16A");
            symbolEnums.Add("cut", "\uE16B");
            symbolEnums.Add("attach", "\uE16C");
            symbolEnums.Add("filter", "\uE16E");
            symbolEnums.Add("copy", "\uE16F");
            symbolEnums.Add("paste", "\uE16D");            
            symbolEnums.Add("emoji2", "\uE170");
            symbolEnums.Add("important", "\uE171");
            symbolEnums.Add("mailreply", "\uE172");
            symbolEnums.Add("slideshow", "\uE173");
            symbolEnums.Add("sort", "\uE174");
            symbolEnums.Add("allapps", "\uE179");
            symbolEnums.Add("disconnectdrive", "\uE17A");
            symbolEnums.Add("mapdrive", "\uE17B");
            symbolEnums.Add("newwindow", "\uE17C");
            symbolEnums.Add("openwith", "\uE17D");
            symbolEnums.Add("contactpresence", "\uE181");
            symbolEnums.Add("priority", "\uE182");
            symbolEnums.Add("uploadskydrive", "\uE183");
            symbolEnums.Add("gototoday", "\uE184");
            symbolEnums.Add("font", "\uE185");
            symbolEnums.Add("fontcolor", "\uE186");
            symbolEnums.Add("contact2", "\uE187");
            symbolEnums.Add("folder", "\uE188");
            symbolEnums.Add("audio", "\uE189");
            symbolEnums.Add("placeholder", "\uE18A");
            symbolEnums.Add("view", "\uE18B");
            symbolEnums.Add("setlockscreen", "\uE18C");
            symbolEnums.Add("settile", "\uE18D");
            symbolEnums.Add("cc", "\uE190");
            symbolEnums.Add("stopslideshow", "\uE191");
            symbolEnums.Add("permissions", "\uE192");
            symbolEnums.Add("highlight", "\uE193");
            symbolEnums.Add("disableupdates", "\uE194");
            symbolEnums.Add("unfavorite", "\uE195");
            symbolEnums.Add("unpin", "\uE196");
            symbolEnums.Add("openlocal", "\uE197");
            symbolEnums.Add("mute", "\uE198");
            symbolEnums.Add("italic", "\uE199");
            symbolEnums.Add("underline", "\uE19A");
            symbolEnums.Add("bold", "\uE19B");
            symbolEnums.Add("movetofolder", "\uE19C");
            symbolEnums.Add("likedislike", "\uE19D");
            symbolEnums.Add("dislike", "\uE19E");
            symbolEnums.Add("like", "\uE19F");
            symbolEnums.Add("alignright", "\uE1A0");
            symbolEnums.Add("aligncenter", "\uE1A1");
            symbolEnums.Add("alignleft", "\uE1A2");
            symbolEnums.Add("zoom", "\uE1A3");
            symbolEnums.Add("zoomout", "\uE1A4");
            symbolEnums.Add("openfile", "\uE1A5");
            symbolEnums.Add("otheruser", "\uE1A6");
            symbolEnums.Add("admin", "\uE1A7");
            symbolEnums.Add("street", "\uE1C3");
            symbolEnums.Add("map", "\uE1C4");
            symbolEnums.Add("clearselection", "\uE1C5");
            symbolEnums.Add("fontdecrease", "\uE1C6");
            symbolEnums.Add("fontincrease", "\uE1C7");
            symbolEnums.Add("fontsize", "\uE1C8");
            symbolEnums.Add("cellphone", "\uE1C9");
            symbolEnums.Add("reshare", "\uE1CA");
            symbolEnums.Add("tag", "\uE1CB");
            symbolEnums.Add("repeatone", "\uE1CC");
            symbolEnums.Add("repeatall", "\uE1CD");
            symbolEnums.Add("outlinestar", "\uE1CE");
            symbolEnums.Add("solidstar", "\uE1CF");
            symbolEnums.Add("calculator", "\uE1D0");
            symbolEnums.Add("directions", "\uE1D1");
            symbolEnums.Add("target", "\uE1D2");
            symbolEnums.Add("library", "\uE1D3");
            symbolEnums.Add("phonebook", "\uE1D4");
            symbolEnums.Add("memo", "\uE1A5");
            symbolEnums.Add("microphone", "\uE1A6");
            symbolEnums.Add("postupdate", "\uE1D7");
            symbolEnums.Add("backtowindow", "\uE1D8");
            symbolEnums.Add("fullscreen", "\uE1D9");
            symbolEnums.Add("newfolder", "\uE1DA");
            symbolEnums.Add("calendarreply", "\uE1DB");
            symbolEnums.Add("unsyncfolder", "\uE1DD");
            symbolEnums.Add("reporthacked", "\uE1DE");
            symbolEnums.Add("syncfolder", "\uE1DF");
            symbolEnums.Add("blockcontact", "\uE1E0");
            symbolEnums.Add("switchapps", "\uE1E1");
            symbolEnums.Add("addfriend", "\uE1E2");
            symbolEnums.Add("touchpointer", "\uE1E3");
            symbolEnums.Add("gotostart", "\uE1E4");
            symbolEnums.Add("zerobars", "\uE1E5");
            symbolEnums.Add("onebar", "\uE1E6");
            symbolEnums.Add("twobars", "\uE1E7");
            symbolEnums.Add("threebars", "\uE1E8");
            symbolEnums.Add("fourbars", "\uE1E9");
            symbolEnums.Add("scan", "\uE294");
            symbolEnums.Add("preview", "\uE295");
        }
        public bool IsIconAvailable(string iconEnum)
        {
            return symbolEnums.ContainsKey(iconEnum);
        }

        public Uri GetIconUri(string iconEnum)
        {
            return new Uri("Assets/AppBar/enums/" + iconEnum + ".png", UriKind.Relative);
        }

        public string GetIconChar(string iconEnum)
        {
            if (symbolEnums.ContainsKey(iconEnum))
                return symbolEnums[iconEnum];
            else
                return "no icon found";
        }
    }
}
