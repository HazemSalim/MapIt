var OxO9e65=["top","hiddenDirectory","hiddenFile","hiddenAlert","hiddenAction","hiddenActionData","This function is disabled in the demo mode.","disabled","[[Disabled]]","[[SpecifyNewFolderName]]","","value","createdir","[[CopyMoveto]]","/","move","copy","[[AreyouSureDelete]]","parentNode","text","isdir","true",".","[[SpecifyNewFileName]]","rename","True","False",":","path","FoldersAndFiles","TR","length","onmouseover","this.style.backgroundColor=\x27#eeeeee\x27;","onmouseout","this.style.backgroundColor=\x27\x27;","nodeName","INPUT","changedir","url","TargetUrl","htmlcode","onload","getElementsByTagName","table","sortable"," ","className","id","rows","cells","innerHTML","\x3Ca href=\x22#\x22 onclick=\x22ts_resortTable(this);return false;\x22\x3E","\x3Cspan class=\x22sortarrow\x22\x3E\x26nbsp;\x3C/span\x3E\x3C/a\x3E","string","undefined","innerText","childNodes","nodeValue","nodeType","span","cellIndex","TABLE","sortdir","down","\x26uarr;","up","\x26darr;","sortbottom","tBodies","sortarrow","\x26nbsp;","20","19","browse_Frame","Image1","FolderDescription","CreateDir","Copy","Move","Delete","DoRefresh","name_Cell","size_Cell","op_Cell","divpreview","sel_target","inp_color","inp_color_preview","inc_class","inp_id","inp_index","inp_access","Table8","inp_title","btn_zoom_in","btn_zoom_out","btn_Actualsize","a","editor","documentElement","documentMode","clientHeight","scrollHeight","width","style","255px","appName","Microsoft Internet Explorer","userAgent","MSIE ([0-9]{1,}[.0-9]{0,})","color","backgroundColor","class","title","target","tabIndex","accessKey","href","href_cetemp",".jpeg",".jpg",".gif",".png","\x3CIMG src=\x27","\x27\x3E",".bmp","\x26nbsp;\x3Cembed src=\x22","\x22 quality=\x22high\x22 width=\x22200\x22 height=\x22200\x22 type=\x22application/x-shockwave-flash\x22 pluginspage=\x22http://www.macromedia.com/go/getflashplayer\x22\x3E\x3C/embed\x3E\x0A",".swf",".avi",".mpg",".mp3","\x26nbsp;\x3Cembed name=\x22MediaPlayer1\x22 src=\x22","\x22 autostart=-1 showcontrols=-1  type=\x22application/x-mplayer2\x22 width=\x22240\x22 height=\x22200\x22 pluginspage=\x22http://www.microsoft.com/Windows/MediaPlayer\x22 \x3E\x3C/embed\x3E\x0A",".mpeg","\x3Cdiv\x3E\x3C/div\x3E","\x3Cdiv\x3E\x26nbsp;\x3C/div\x3E","\x3Cdiv\x3E\x26#160;\x3C/div\x3E","\x3Cp\x3E\x3C/p\x3E","\x3Cp\x3E\x26#160;\x3C/p\x3E","\x3Cp\x3E\x26nbsp;\x3C/p\x3E","name","zoom","onclick","display","none","align","absmiddle","wrapupPrompt","iepromptfield","body","div","IEPromptBox","promptBlackout","border","1px solid #b0bec7","#f0f0f0","position","absolute","330px","zIndex","100","\x3Cdiv style=\x22width: 100%; padding-top:3px;background-color: #DCE7EB; font-family: verdana; font-size: 10pt; font-weight: bold; height: 22px; text-align:center; background:url(Load.ashx?type=image\x26file=formbg2.gif) repeat-x left top;\x22\x3E[[InputRequired]]\x3C/div\x3E","\x3Cdiv style=\x22padding: 10px\x22\x3E","\x3CBR\x3E\x3CBR\x3E","\x3Cform action=\x22\x22 onsubmit=\x22return wrapupPrompt()\x22\x3E","\x3Cinput id=\x22iepromptfield\x22 name=\x22iepromptdata\x22 type=text size=46 value=\x22","\x22\x3E","\x3Cbr\x3E\x3Cbr\x3E\x3Ccenter\x3E","\x3Cinput type=\x22submit\x22 value=\x22\x26nbsp;\x26nbsp;\x26nbsp;[[OK]]\x26nbsp;\x26nbsp;\x26nbsp;\x22\x3E","\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;","\x3Cinput type=\x22button\x22 onclick=\x22wrapupPrompt(true)\x22 value=\x22\x26nbsp;[[Cancel]]\x26nbsp;\x22\x3E","\x3C/form\x3E\x3C/div\x3E","100px","left","offsetWidth","px","block","CuteEditor_ColorPicker_ButtonOver(this)"];function Window_GetDialogTop(Ox1a8){return Ox1a8[OxO9e65[0]];} ;var hiddenDirectory=Window_GetElement(window,OxO9e65[1],true);var hiddenFile=Window_GetElement(window,OxO9e65[2],true);var hiddenAlert=Window_GetElement(window,OxO9e65[3],true);var hiddenAction=Window_GetElement(window,OxO9e65[4],true);var hiddenActionData=Window_GetElement(window,OxO9e65[5],true);function CreateDir_click(){if(isDemoMode){alert(OxO9e65[6]);return false;} ;if(Event_GetSrcElement()[OxO9e65[7]]){alert(OxO9e65[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO9e65[9],OxO9e65[10]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxO9e65[11]]=Ox382;hiddenAction[OxO9e65[11]]=OxO9e65[12];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO9e65[9],OxO9e65[10]);if(Ox382){hiddenActionData[OxO9e65[11]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Move_click(){if(isDemoMode){alert(OxO9e65[6]);return false;} ;if(Event_GetSrcElement()[OxO9e65[7]]){alert(OxO9e65[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO9e65[13],OxO9e65[14]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxO9e65[11]]=Ox382;hiddenAction[OxO9e65[11]]=OxO9e65[15];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO9e65[13],OxO9e65[14]);if(Ox382){hiddenActionData[OxO9e65[11]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Copy_click(){if(isDemoMode){alert(OxO9e65[6]);return false;} ;if(Event_GetSrcElement()[OxO9e65[7]]){alert(OxO9e65[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO9e65[13],OxO9e65[14]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxO9e65[11]]=Ox382;hiddenAction[OxO9e65[11]]=OxO9e65[16];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO9e65[13],OxO9e65[14]);if(Ox382){hiddenActionData[OxO9e65[11]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Delete_click(){if(isDemoMode){alert(OxO9e65[6]);return false;} ;if(Event_GetSrcElement()[OxO9e65[7]]){alert(OxO9e65[8]);return false;} ;return confirm(OxO9e65[17]);} ;function EditImg_click(img){if(isDemoMode){alert(OxO9e65[6]);return false;} ;if(img[OxO9e65[7]]){alert(OxO9e65[8]);return false;} ;var Ox387=img[OxO9e65[18]][OxO9e65[18]];var Ox388=Ox387.getAttribute(OxO9e65[19]);var name;var Ox389;Ox389=Ox387.getAttribute(OxO9e65[20])==OxO9e65[21];if(Browser_IsIE7()){var Oxca;if(Ox389){IEprompt(Ox221,OxO9e65[9],Ox388);} else {var i=Ox388.lastIndexOf(OxO9e65[22]);Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxO9e65[22]));IEprompt(Ox221,OxO9e65[23],Ox12);} ;function Ox221(Ox382){if(Ox382&&Ox382!=Ox387.getAttribute(OxO9e65[19])){if(!Ox389){Ox382=Ox382+Oxca;} ;hiddenAction[OxO9e65[11]]=OxO9e65[24];hiddenActionData[OxO9e65[11]]=(Ox389?OxO9e65[25]:OxO9e65[26])+OxO9e65[27]+Ox387.getAttribute(OxO9e65[28])+OxO9e65[27]+Ox382;window.PostBackAction();} ;} ;} else {if(Ox389){name=prompt(OxO9e65[9],Ox388);} else {var i=Ox388.lastIndexOf(OxO9e65[22]);var Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxO9e65[22]));name=prompt(OxO9e65[23],Ox12);if(name){name=name+Oxca;} ;} ;if(name&&name!=Ox387.getAttribute(OxO9e65[19])){hiddenAction[OxO9e65[11]]=OxO9e65[24];hiddenActionData[OxO9e65[11]]=(Ox389?OxO9e65[25]:OxO9e65[26])+OxO9e65[27]+Ox387.getAttribute(OxO9e65[28])+OxO9e65[27]+name;window.PostBackAction();} ;} ;return Event_CancelEvent();} ;setMouseOver();function setMouseOver(){var FoldersAndFiles=Window_GetElement(window,OxO9e65[29],true);var Ox38c=FoldersAndFiles.getElementsByTagName(OxO9e65[30]);for(var i=1;i<Ox38c[OxO9e65[31]];i++){var Ox387=Ox38c[i];Ox387[OxO9e65[32]]= new Function(OxO9e65[10],OxO9e65[33]);Ox387[OxO9e65[34]]= new Function(OxO9e65[10],OxO9e65[35]);} ;} ;function row_click(Ox387){var Ox389;Ox389=Ox387.getAttribute(OxO9e65[20])==OxO9e65[21];if(Ox389){if(Event_GetSrcElement()[OxO9e65[36]]==OxO9e65[37]){return ;} ;hiddenAction[OxO9e65[11]]=OxO9e65[38];hiddenActionData[OxO9e65[11]]=Ox387.getAttribute(OxO9e65[28]);window.PostBackAction();} else {var Ox109=Ox387.getAttribute(OxO9e65[28]);hiddenFile[OxO9e65[11]]=Ox109;var Ox288=Ox387.getAttribute(OxO9e65[39]);Window_GetElement(window,OxO9e65[40],true)[OxO9e65[11]]=Ox288;var htmlcode=Ox387.getAttribute(OxO9e65[41]);if(htmlcode!=OxO9e65[10]&&htmlcode!=null){do_preview(htmlcode);} else {try{Actualsize();} catch(x){do_preview();} ;} ;} ;} ;function do_preview(){} ;function reset_hiddens(){if(hiddenAlert[OxO9e65[11]]){alert(hiddenAlert.value);} ;if(TargetUrl[OxO9e65[11]]!=OxO9e65[10]&&TargetUrl[OxO9e65[11]]!=null){do_preview();} ;hiddenAlert[OxO9e65[11]]=OxO9e65[10];hiddenAction[OxO9e65[11]]=OxO9e65[10];hiddenActionData[OxO9e65[11]]=OxO9e65[10];} ;Event_Attach(window,OxO9e65[42],reset_hiddens);function RequireFileBrowseScript(){} ;Event_Attach(window,OxO9e65[42],sortables_init);var SORT_COLUMN_INDEX;function sortables_init(){if(!document[OxO9e65[43]]){return ;} ;var Ox391=document.getElementsByTagName(OxO9e65[44]);for(var Ox392=0;Ox392<Ox391[OxO9e65[31]];Ox392++){var Ox393=Ox391[Ox392];if(((OxO9e65[46]+Ox393[OxO9e65[47]]+OxO9e65[46]).indexOf(OxO9e65[45])!=-1)&&(Ox393[OxO9e65[48]])){ts_makeSortable(Ox393);} ;} ;} ;function ts_makeSortable(Ox395){if(Ox395[OxO9e65[49]]&&Ox395[OxO9e65[49]][OxO9e65[31]]>0){var Ox396=Ox395[OxO9e65[49]][0];} ;if(!Ox396){return ;} ;for(var i=2;i<4;i++){var Ox397=Ox396[OxO9e65[50]][i];var Ox219=ts_getInnerText(Ox397);Ox397[OxO9e65[51]]=OxO9e65[52]+Ox219+OxO9e65[53];} ;} ;function ts_getInnerText(Ox29){if( typeof Ox29==OxO9e65[54]){return Ox29;} ;if( typeof Ox29==OxO9e65[55]){return Ox29;} ;if(Ox29[OxO9e65[56]]){return Ox29[OxO9e65[56]];} ;var Ox24=OxO9e65[10];var Ox343=Ox29[OxO9e65[57]];var Ox11=Ox343[OxO9e65[31]];for(var i=0;i<Ox11;i++){switch(Ox343[i][OxO9e65[59]]){case 1:Ox24+=ts_getInnerText(Ox343[i]);break ;;case 3:Ox24+=Ox343[i][OxO9e65[58]];break ;;} ;} ;return Ox24;} ;function ts_resortTable(Ox39a){var Ox2a6;for(var Ox39b=0;Ox39b<Ox39a[OxO9e65[57]][OxO9e65[31]];Ox39b++){if(Ox39a[OxO9e65[57]][Ox39b][OxO9e65[36]]&&Ox39a[OxO9e65[57]][Ox39b][OxO9e65[36]].toLowerCase()==OxO9e65[60]){Ox2a6=Ox39a[OxO9e65[57]][Ox39b];} ;} ;var Ox39c=ts_getInnerText(Ox2a6);var Ox1e4=Ox39a[OxO9e65[18]];var Ox39d=Ox1e4[OxO9e65[61]];var Ox395=getParent(Ox1e4,OxO9e65[62]);if(Ox395[OxO9e65[49]][OxO9e65[31]]<=1){return ;} ;var Ox39e=ts_getInnerText(Ox395[OxO9e65[49]][1][OxO9e65[50]][Ox39d]);var Ox39f=ts_sort_caseinsensitive;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^[?]/)){Ox39f=ts_sort_currency;} ;if(Ox39e.match(/^[\d\.]+$/)){Ox39f=ts_sort_numeric;} ;SORT_COLUMN_INDEX=Ox39d;var Ox396= new Array();var Ox3a0= new Array();for(var i=0;i<Ox395[OxO9e65[49]][0][OxO9e65[31]];i++){Ox396[i]=Ox395[OxO9e65[49]][0][i];} ;for(var Ox25=1;Ox25<Ox395[OxO9e65[49]][OxO9e65[31]];Ox25++){Ox3a0[Ox25-1]=Ox395[OxO9e65[49]][Ox25];} ;Ox3a0.sort(Ox39f);if(Ox2a6.getAttribute(OxO9e65[63])==OxO9e65[64]){var Ox3a1=OxO9e65[65];Ox3a0.reverse();Ox2a6.setAttribute(OxO9e65[63],OxO9e65[66]);} else {Ox3a1=OxO9e65[67];Ox2a6.setAttribute(OxO9e65[63],OxO9e65[64]);} ;for(i=0;i<Ox3a0[OxO9e65[31]];i++){if(!Ox3a0[i][OxO9e65[47]]||(Ox3a0[i][OxO9e65[47]]&&(Ox3a0[i][OxO9e65[47]].indexOf(OxO9e65[68])==-1))){Ox395[OxO9e65[69]][0].appendChild(Ox3a0[i]);} ;} ;for(i=0;i<Ox3a0[OxO9e65[31]];i++){if(Ox3a0[i][OxO9e65[47]]&&(Ox3a0[i][OxO9e65[47]].indexOf(OxO9e65[68])!=-1)){Ox395[OxO9e65[69]][0].appendChild(Ox3a0[i]);} ;} ;var Ox3a2=document.getElementsByTagName(OxO9e65[60]);for(var Ox39b=0;Ox39b<Ox3a2[OxO9e65[31]];Ox39b++){if(Ox3a2[Ox39b][OxO9e65[47]]==OxO9e65[70]){if(getParent(Ox3a2[Ox39b],OxO9e65[44])==getParent(Ox39a,OxO9e65[44])){Ox3a2[Ox39b][OxO9e65[51]]=OxO9e65[71];} ;} ;} ;Ox2a6[OxO9e65[51]]=Ox3a1;} ;function getParent(Ox29,Ox3a4){if(Ox29==null){return null;} else {if(Ox29[OxO9e65[59]]==1&&Ox29[OxO9e65[36]].toLowerCase()==Ox3a4.toLowerCase()){return Ox29;} else {return getParent(Ox29.parentNode,Ox3a4);} ;} ;} ;function ts_sort_date(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO9e65[50]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxO9e65[50]][SORT_COLUMN_INDEX]);if(Ox3a6[OxO9e65[31]]==10){var Ox3a8=Ox3a6.substr(6,4)+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} else {var Ox3a9=Ox3a6.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxO9e65[72]+Ox3a9;} else {Ox3a9=OxO9e65[73]+Ox3a9;} ;var Ox3a8=Ox3a9+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} ;if(Ox3a7[OxO9e65[31]]==10){var Ox3aa=Ox3a7.substr(6,4)+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} else {Ox3a9=Ox3a7.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxO9e65[72]+Ox3a9;} else {Ox3a9=OxO9e65[73]+Ox3a9;} ;var Ox3aa=Ox3a9+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} ;if(Ox3a8==Ox3aa){return 0;} ;if(Ox3a8<Ox3aa){return -1;} ;return 1;} ;function ts_sort_currency(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO9e65[50]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxO9e65[10]);var Ox3a7=ts_getInnerText(b[OxO9e65[50]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxO9e65[10]);return parseFloat(Ox3a6)-parseFloat(Ox3a7);} ;function ts_sort_numeric(Oxee,b){var Ox3a6=parseFloat(ts_getInnerText(Oxee[OxO9e65[50]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a6)){Ox3a6=0;} ;var Ox3a7=parseFloat(ts_getInnerText(b[OxO9e65[50]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a7)){Ox3a7=0;} ;return Ox3a6-Ox3a7;} ;function ts_sort_caseinsensitive(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO9e65[50]][SORT_COLUMN_INDEX]).toLowerCase();var Ox3a7=ts_getInnerText(b[OxO9e65[50]][SORT_COLUMN_INDEX]).toLowerCase();if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} ;function ts_sort_default(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO9e65[50]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxO9e65[50]][SORT_COLUMN_INDEX]);if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} [sortables_init];RequireFileBrowseScript();var browse_Frame=Window_GetElement(window,OxO9e65[74],true);var hiddenDirectory=Window_GetElement(window,OxO9e65[1],true);var hiddenFile=Window_GetElement(window,OxO9e65[2],true);var hiddenAlert=Window_GetElement(window,OxO9e65[3],true);var hiddenAction=Window_GetElement(window,OxO9e65[4],true);var hiddenActionData=Window_GetElement(window,OxO9e65[5],true);var Image1=Window_GetElement(window,OxO9e65[75],true);var FolderDescription=Window_GetElement(window,OxO9e65[76],true);var CreateDir=Window_GetElement(window,OxO9e65[77],true);var Copy=Window_GetElement(window,OxO9e65[78],true);var Move=Window_GetElement(window,OxO9e65[79],true);var FoldersAndFiles=Window_GetElement(window,OxO9e65[29],true);var Delete=Window_GetElement(window,OxO9e65[80],true);var DoRefresh=Window_GetElement(window,OxO9e65[81],true);var name_Cell=Window_GetElement(window,OxO9e65[82],true);var size_Cell=Window_GetElement(window,OxO9e65[83],true);var op_Cell=Window_GetElement(window,OxO9e65[84],true);var divpreview=Window_GetElement(window,OxO9e65[85],true);var sel_target=Window_GetElement(window,OxO9e65[86],true);var inp_color=Window_GetElement(window,OxO9e65[87],true);var inp_color_preview=Window_GetElement(window,OxO9e65[88],true);var inc_class=Window_GetElement(window,OxO9e65[89],true);var inp_id=Window_GetElement(window,OxO9e65[90],true);var inp_index=Window_GetElement(window,OxO9e65[91],true);var inp_access=Window_GetElement(window,OxO9e65[92],true);var Table8=Window_GetElement(window,OxO9e65[93],true);var TargetUrl=Window_GetElement(window,OxO9e65[40],true);var inp_title=Window_GetElement(window,OxO9e65[94],true);var btn_zoom_in=Window_GetElement(window,OxO9e65[95],true);var btn_zoom_out=Window_GetElement(window,OxO9e65[96],true);var btn_Actualsize=Window_GetElement(window,OxO9e65[97],true);var obj=Window_GetDialogArguments(window);var element=null;if(obj){element=obj[OxO9e65[98]];} ;var editor=obj[OxO9e65[99]];var ver=getInternetExplorerVersion();if(ver>-1&&ver<=9.0){var needAdjust=true;if(ver>=8.0&&document[OxO9e65[100]]){if(document[OxO9e65[101]]>7){needAdjust=false;} ;} ;if(needAdjust&&(browse_Frame[OxO9e65[102]]<browse_Frame[OxO9e65[103]])){FoldersAndFiles[OxO9e65[105]][OxO9e65[104]]=OxO9e65[106];} ;} ;function getInternetExplorerVersion(){var Ox3ca=-1;if(navigator[OxO9e65[107]]==OxO9e65[108]){var Ox3cb=navigator[OxO9e65[109]];var Ox296= new RegExp(OxO9e65[110]);if(Ox296.exec(Ox3cb)!=null){Ox3ca=parseFloat(RegExp.$1);} ;} ;return Ox3ca;} ;var htmlcode=OxO9e65[10];if(element[OxO9e65[105]][OxO9e65[111]]){inp_color[OxO9e65[11]]=revertColor(element[OxO9e65[105]].color);inp_color[OxO9e65[105]][OxO9e65[112]]=inp_color[OxO9e65[11]];inp_color_preview[OxO9e65[105]][OxO9e65[112]]=inp_color[OxO9e65[11]];} ;if(element[OxO9e65[47]]==OxO9e65[10]){element.removeAttribute(OxO9e65[47]);} ;if(element[OxO9e65[47]]==OxO9e65[10]){element.removeAttribute(OxO9e65[113]);} ;if(element[OxO9e65[114]]){inp_title[OxO9e65[11]]=element[OxO9e65[114]];} ;if(element[OxO9e65[115]]){sel_target[OxO9e65[11]]=element[OxO9e65[115]];} ;if(element[OxO9e65[116]]){inp_index[OxO9e65[11]]=element[OxO9e65[116]];} ;if(element[OxO9e65[117]]){inp_access[OxO9e65[11]]=element[OxO9e65[117]];} ;var src=OxO9e65[10];if(element.getAttribute(OxO9e65[118])){src=element.getAttribute(OxO9e65[118]);} ;if(element.getAttribute(OxO9e65[119])){src=element.getAttribute(OxO9e65[119]);} ;if(TargetUrl[OxO9e65[11]]){Actualsize();} else {if(element&&src){TargetUrl[OxO9e65[11]]=src;} ;} ;inp_id[OxO9e65[11]]=element[OxO9e65[48]];var divpreview=Window_GetElement(window,OxO9e65[85],true);do_preview();function do_preview(Ox283){if(Ox283!=OxO9e65[10]&&Ox283!=null){htmlcode=Ox283;divpreview[OxO9e65[51]]=Ox283;return ;} ;divpreview[OxO9e65[51]]=OxO9e65[10];var Ox288=TargetUrl[OxO9e65[11]];if(Ox288==OxO9e65[10]){return ;} ;var Oxca=Ox288.substring(Ox288.lastIndexOf(OxO9e65[22])).toLowerCase();switch(Oxca){case OxO9e65[120]:;case OxO9e65[121]:;case OxO9e65[122]:;case OxO9e65[123]:;case OxO9e65[126]:divpreview[OxO9e65[51]]=OxO9e65[124]+Ox288+OxO9e65[125];break ;;case OxO9e65[129]:var Ox3cc=OxO9e65[127]+Ox288+OxO9e65[128];divpreview[OxO9e65[51]]=Ox3cc+OxO9e65[71];break ;;case OxO9e65[130]:;case OxO9e65[131]:;case OxO9e65[132]:;case OxO9e65[135]:var Ox3cd=OxO9e65[133]+Ox288+OxO9e65[134];divpreview[OxO9e65[51]]=Ox3cd+OxO9e65[71];break ;;} ;} ;function do_insert(){element[OxO9e65[47]]=inc_class[OxO9e65[11]];element[OxO9e65[115]]=sel_target[OxO9e65[11]];element[OxO9e65[114]]=inp_title[OxO9e65[11]];if(TargetUrl[OxO9e65[11]]){element[OxO9e65[118]]=TargetUrl[OxO9e65[11]];element.setAttribute(OxO9e65[119],TargetUrl.value);} ;element[OxO9e65[116]]=inp_index[OxO9e65[11]];element[OxO9e65[117]]=inp_access[OxO9e65[11]];element[OxO9e65[48]]=inp_id[OxO9e65[11]];if(element[OxO9e65[114]]==OxO9e65[10]){element.removeAttribute(OxO9e65[114]);} ;if(element[OxO9e65[115]]==OxO9e65[10]){element.removeAttribute(OxO9e65[115]);} ;if(element[OxO9e65[47]]==OxO9e65[10]){element.removeAttribute(OxO9e65[47]);} ;if(element[OxO9e65[47]]==OxO9e65[10]){element.removeAttribute(OxO9e65[113]);} ;if(element[OxO9e65[116]]==OxO9e65[10]){element.removeAttribute(OxO9e65[116]);} ;if(element[OxO9e65[117]]==OxO9e65[10]){element.removeAttribute(OxO9e65[117]);} ;if(element[OxO9e65[48]]==OxO9e65[10]){element.removeAttribute(OxO9e65[48]);} ;try{element[OxO9e65[105]][OxO9e65[111]]=inp_color[OxO9e65[11]];} catch(er){element[OxO9e65[105]][OxO9e65[111]]=OxO9e65[10];} ;var Ox283=element[OxO9e65[51]];switch(Ox283.toLowerCase()){case OxO9e65[136]:;case OxO9e65[137]:;case OxO9e65[138]:;case OxO9e65[139]:;case OxO9e65[140]:;case OxO9e65[141]:element[OxO9e65[51]]=OxO9e65[10];break ;;default:break ;;} ;if(element[OxO9e65[51]]==OxO9e65[10]){element[OxO9e65[51]]=element[OxO9e65[114]]||TargetUrl[OxO9e65[11]]||element[OxO9e65[142]]||OxO9e65[10];} ;Window_SetDialogReturnValue(window,element);Window_CloseDialog(window);} ;function do_Close(){Window_SetDialogReturnValue(window,null);Window_CloseDialog(window);} ;function Zoom_In(){if(divpreview[OxO9e65[105]][OxO9e65[143]]!=0){divpreview[OxO9e65[105]][OxO9e65[143]]*=1.2;} else {divpreview[OxO9e65[105]][OxO9e65[143]]=1.2;} ;} ;function Zoom_Out(){if(divpreview[OxO9e65[105]][OxO9e65[143]]!=0){divpreview[OxO9e65[105]][OxO9e65[143]]*=0.8;} else {divpreview[OxO9e65[105]][OxO9e65[143]]=0.8;} ;} ;function Actualsize(){divpreview[OxO9e65[105]][OxO9e65[143]]=1;do_preview();} ;inp_color[OxO9e65[144]]=inp_color_preview[OxO9e65[144]]=function inp_color_onclick(){SelectColor(inp_color,inp_color_preview);} ;if(!Browser_IsWinIE()){btn_zoom_in[OxO9e65[105]][OxO9e65[145]]=btn_zoom_out[OxO9e65[105]][OxO9e65[145]]=btn_Actualsize[OxO9e65[105]][OxO9e65[145]]=OxO9e65[146];inp_color_preview.setAttribute(OxO9e65[147],OxO9e65[148]);} ;if(Browser_IsIE7()){var _dialogPromptID=null;function IEprompt(Ox221,Ox222,Ox223){that=this;this[OxO9e65[149]]=function (Ox224){val=document.getElementById(OxO9e65[150])[OxO9e65[11]];_dialogPromptID[OxO9e65[105]][OxO9e65[145]]=OxO9e65[146];document.getElementById(OxO9e65[150])[OxO9e65[11]]=OxO9e65[10];if(Ox224){val=OxO9e65[10];} ;Ox221(val);return false;} ;if(Ox223==undefined){Ox223=OxO9e65[10];} ;if(_dialogPromptID==null){var Ox225=document.getElementsByTagName(OxO9e65[151])[0];tnode=document.createElement(OxO9e65[152]);tnode[OxO9e65[48]]=OxO9e65[153];Ox225.appendChild(tnode);_dialogPromptID=document.getElementById(OxO9e65[153]);tnode=document.createElement(OxO9e65[152]);tnode[OxO9e65[48]]=OxO9e65[154];Ox225.appendChild(tnode);_dialogPromptID[OxO9e65[105]][OxO9e65[155]]=OxO9e65[156];_dialogPromptID[OxO9e65[105]][OxO9e65[112]]=OxO9e65[157];_dialogPromptID[OxO9e65[105]][OxO9e65[158]]=OxO9e65[159];_dialogPromptID[OxO9e65[105]][OxO9e65[104]]=OxO9e65[160];_dialogPromptID[OxO9e65[105]][OxO9e65[161]]=OxO9e65[162];} ;var Ox226=OxO9e65[163];Ox226+=OxO9e65[164]+Ox222+OxO9e65[165];Ox226+=OxO9e65[166];Ox226+=OxO9e65[167]+Ox223+OxO9e65[168];Ox226+=OxO9e65[169];Ox226+=OxO9e65[170];Ox226+=OxO9e65[171];Ox226+=OxO9e65[172];Ox226+=OxO9e65[173];_dialogPromptID[OxO9e65[51]]=Ox226;_dialogPromptID[OxO9e65[105]][OxO9e65[0]]=OxO9e65[174];_dialogPromptID[OxO9e65[105]][OxO9e65[175]]=parseInt((document[OxO9e65[151]][OxO9e65[176]]-315)/2)+OxO9e65[177];_dialogPromptID[OxO9e65[105]][OxO9e65[145]]=OxO9e65[178];var Ox227=document.getElementById(OxO9e65[150]);try{var Ox228=Ox227.createTextRange();Ox228.collapse(false);Ox228.select();} catch(x){Ox227.focus();} ;} ;} ;if(CreateDir){CreateDir[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(Copy){Copy[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(Move){Move[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(Delete){Delete[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(DoRefresh){DoRefresh[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(btn_zoom_in){btn_zoom_in[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(btn_zoom_out){btn_zoom_out[OxO9e65[32]]= new Function(OxO9e65[179]);} ;if(btn_Actualsize){btn_Actualsize[OxO9e65[32]]= new Function(OxO9e65[179]);} ;