var OxOcc14=["hiddenDirectory","hiddenFile","hiddenAlert","hiddenAction","hiddenActionData","This function is disabled in the demo mode.","disabled","[[Disabled]]","[[SpecifyNewFolderName]]","","value","createdir","[[CopyMoveto]]","/","move","copy","[[AreyouSureDelete]]","parentNode","isdir","true","text",".","[[SpecifyNewFileName]]","rename","True","False",":","path","FoldersAndFiles","TR","length","onmouseover","this.style.backgroundColor=\x27#eeeeee\x27;","onmouseout","this.style.backgroundColor=\x27\x27;","nodeName","INPUT","changedir","url","TargetUrl","htmlcode","onload","getElementsByTagName","table","sortable"," ","className","id","rows","cells","innerHTML","\x3Ca href=\x22#\x22 onclick=\x22ts_resortTable(this);return false;\x22\x3E","\x3Cspan class=\x22sortarrow\x22\x3E\x26nbsp;\x3C/span\x3E\x3C/a\x3E","string","undefined","innerText","childNodes","nodeValue","nodeType","span","cellIndex","TABLE","sortdir","down","\x26uarr;","up","\x26darr;","sortbottom","tBodies","sortarrow","\x26nbsp;","20","19","browse_Frame","Image1","FolderDescription","CreateDir","Copy","Move","Delete","DoRefresh","divpreview","Button1","Button2","btn_zoom_in","btn_zoom_out","btn_Actualsize","editor","window","document","documentElement","documentMode","clientHeight","scrollHeight","width","style","255px","appName","Microsoft Internet Explorer","userAgent","MSIE ([0-9]{1,}[.0-9]{0,})",".jpeg",".jpg",".gif",".png","\x3CIMG src=\x27","\x27\x3E",".bmp","\x26nbsp;\x3Cembed src=\x22","\x22 quality=\x22high\x22 width=\x22200\x22 height=\x22200\x22 type=\x22application/x-shockwave-flash\x22 pluginspage=\x22http://www.macromedia.com/go/getflashplayer\x22\x3E\x3C/embed\x3E\x0A",".swf",".avi",".mpg",".mp3","\x26nbsp;\x3Cembed name=\x22MediaPlayer1\x22 src=\x22","\x22 autostart=-1 showcontrols=-1  type=\x22application/x-mplayer2\x22 width=\x22240\x22 height=\x22200\x22 pluginspage=\x22http://www.microsoft.com/Windows/MediaPlayer\x22 \x3E\x3C/embed\x3E\x0A",".mpeg","inp","zoom","display","none","wrapupPrompt","iepromptfield","body","div","IEPromptBox","promptBlackout","border","1px solid #b0bec7","backgroundColor","#f0f0f0","position","absolute","330px","zIndex","100","\x3Cdiv style=\x22width: 100%; padding-top:3px;background-color: #DCE7EB; font-family: verdana; font-size: 10pt; font-weight: bold; height: 22px; text-align:center; background:url(Load.ashx?type=image\x26file=formbg2.gif) repeat-x left top;\x22\x3E[[InputRequired]]\x3C/div\x3E","\x3Cdiv style=\x22padding: 10px\x22\x3E","\x3CBR\x3E\x3CBR\x3E","\x3Cform action=\x22\x22 onsubmit=\x22return wrapupPrompt()\x22\x3E","\x3Cinput id=\x22iepromptfield\x22 name=\x22iepromptdata\x22 type=text size=46 value=\x22","\x22\x3E","\x3Cbr\x3E\x3Cbr\x3E\x3Ccenter\x3E","\x3Cinput type=\x22submit\x22 value=\x22\x26nbsp;\x26nbsp;\x26nbsp;[[OK]]\x26nbsp;\x26nbsp;\x26nbsp;\x22\x3E","\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;","\x3Cinput type=\x22button\x22 onclick=\x22wrapupPrompt(true)\x22 value=\x22\x26nbsp;[[Cancel]]\x26nbsp;\x22\x3E","\x3C/form\x3E\x3C/div\x3E","top","100px","left","offsetWidth","px","block","CuteEditor_ColorPicker_ButtonOver(this)"];var hiddenDirectory=Window_GetElement(window,OxOcc14[0],true);var hiddenFile=Window_GetElement(window,OxOcc14[1],true);var hiddenAlert=Window_GetElement(window,OxOcc14[2],true);var hiddenAction=Window_GetElement(window,OxOcc14[3],true);var hiddenActionData=Window_GetElement(window,OxOcc14[4],true);function CreateDir_click(){if(isDemoMode){alert(OxOcc14[5]);return false;} ;if(Event_GetSrcElement()[OxOcc14[6]]){alert(OxOcc14[7]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxOcc14[8],OxOcc14[9]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxOcc14[10]]=Ox382;hiddenAction[OxOcc14[10]]=OxOcc14[11];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxOcc14[8],OxOcc14[9]);if(Ox382){hiddenActionData[OxOcc14[10]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Move_click(){if(isDemoMode){alert(OxOcc14[5]);return false;} ;if(Event_GetSrcElement()[OxOcc14[6]]){alert(OxOcc14[7]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxOcc14[12],OxOcc14[13]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxOcc14[10]]=Ox382;hiddenAction[OxOcc14[10]]=OxOcc14[14];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxOcc14[12],OxOcc14[13]);if(Ox382){hiddenActionData[OxOcc14[10]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Copy_click(){if(isDemoMode){alert(OxOcc14[5]);return false;} ;if(Event_GetSrcElement()[OxOcc14[6]]){alert(OxOcc14[7]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxOcc14[12],OxOcc14[13]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxOcc14[10]]=Ox382;hiddenAction[OxOcc14[10]]=OxOcc14[15];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxOcc14[12],OxOcc14[13]);if(Ox382){hiddenActionData[OxOcc14[10]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Delete_click(){if(isDemoMode){alert(OxOcc14[5]);return false;} ;if(Event_GetSrcElement()[OxOcc14[6]]){alert(OxOcc14[7]);return false;} ;return confirm(OxOcc14[16]);} ;function EditImg_click(img){if(isDemoMode){alert(OxOcc14[5]);return false;} ;if(img[OxOcc14[6]]){alert(OxOcc14[7]);return false;} ;var Ox387=img[OxOcc14[17]][OxOcc14[17]];var Ox389;Ox389=Ox387.getAttribute(OxOcc14[18])==OxOcc14[19];var Ox388=Ox387.getAttribute(OxOcc14[20]);var name;if(Ox389){name=prompt(OxOcc14[8],Ox388);} else {var i=Ox388.lastIndexOf(OxOcc14[21]);var Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxOcc14[21]));name=prompt(OxOcc14[22],Ox12);if(name){name=name+Oxca;} ;} ;if(name&&name!=Ox387.getAttribute(OxOcc14[20])){hiddenAction[OxOcc14[10]]=OxOcc14[23];hiddenActionData[OxOcc14[10]]=(Ox389?OxOcc14[24]:OxOcc14[25])+OxOcc14[26]+Ox387.getAttribute(OxOcc14[27])+OxOcc14[26]+name;window.PostBackAction();} ;return Event_CancelEvent();} ;setMouseOver();function setMouseOver(){var FoldersAndFiles=Window_GetElement(window,OxOcc14[28],true);var Ox38c=FoldersAndFiles.getElementsByTagName(OxOcc14[29]);for(var i=1;i<Ox38c[OxOcc14[30]];i++){var Ox387=Ox38c[i];Ox387[OxOcc14[31]]= new Function(OxOcc14[9],OxOcc14[32]);Ox387[OxOcc14[33]]= new Function(OxOcc14[9],OxOcc14[34]);} ;} ;function row_click(Ox387){var Ox389;Ox389=Ox387.getAttribute(OxOcc14[18])==OxOcc14[19];if(Ox389){if(Event_GetSrcElement()[OxOcc14[35]]==OxOcc14[36]){return ;} ;hiddenAction[OxOcc14[10]]=OxOcc14[37];hiddenActionData[OxOcc14[10]]=Ox387.getAttribute(OxOcc14[27]);window.PostBackAction();} else {var Ox109=Ox387.getAttribute(OxOcc14[27]);hiddenFile[OxOcc14[10]]=Ox109;var Ox288=Ox387.getAttribute(OxOcc14[38]);Window_GetElement(window,OxOcc14[39],true)[OxOcc14[10]]=Ox288;var htmlcode=Ox387.getAttribute(OxOcc14[40]);if(htmlcode!=OxOcc14[9]&&htmlcode!=null){do_preview(htmlcode);} else {try{Actualsize();} catch(x){do_preview();} ;} ;} ;} ;function do_preview(){} ;function reset_hiddens(){if(hiddenAlert[OxOcc14[10]]){alert(hiddenAlert.value);} ;hiddenAlert[OxOcc14[10]]=OxOcc14[9];hiddenAction[OxOcc14[10]]=OxOcc14[9];hiddenActionData[OxOcc14[10]]=OxOcc14[9];} ;Event_Attach(window,OxOcc14[41],reset_hiddens);Event_Attach(window,OxOcc14[41],sortables_init);var SORT_COLUMN_INDEX;function sortables_init(){if(!document[OxOcc14[42]]){return ;} ;var Ox391=document.getElementsByTagName(OxOcc14[43]);for(var Ox392=0;Ox392<Ox391[OxOcc14[30]];Ox392++){var Ox393=Ox391[Ox392];if(((OxOcc14[45]+Ox393[OxOcc14[46]]+OxOcc14[45]).indexOf(OxOcc14[44])!=-1)&&(Ox393[OxOcc14[47]])){ts_makeSortable(Ox393);} ;} ;} ;function ts_makeSortable(Ox395){if(Ox395[OxOcc14[48]]&&Ox395[OxOcc14[48]][OxOcc14[30]]>0){var Ox396=Ox395[OxOcc14[48]][0];} ;if(!Ox396){return ;} ;for(var i=2;i<4;i++){var Ox397=Ox396[OxOcc14[49]][i];var Ox219=ts_getInnerText(Ox397);Ox397[OxOcc14[50]]=OxOcc14[51]+Ox219+OxOcc14[52];} ;} ;function ts_getInnerText(Ox29){if( typeof Ox29==OxOcc14[53]){return Ox29;} ;if( typeof Ox29==OxOcc14[54]){return Ox29;} ;if(Ox29[OxOcc14[55]]){return Ox29[OxOcc14[55]];} ;var Ox24=OxOcc14[9];var Ox343=Ox29[OxOcc14[56]];var Ox11=Ox343[OxOcc14[30]];for(var i=0;i<Ox11;i++){switch(Ox343[i][OxOcc14[58]]){case 1:Ox24+=ts_getInnerText(Ox343[i]);break ;;case 3:Ox24+=Ox343[i][OxOcc14[57]];break ;;} ;} ;return Ox24;} ;function ts_resortTable(Ox39a){var Ox2a6;for(var Ox39b=0;Ox39b<Ox39a[OxOcc14[56]][OxOcc14[30]];Ox39b++){if(Ox39a[OxOcc14[56]][Ox39b][OxOcc14[35]]&&Ox39a[OxOcc14[56]][Ox39b][OxOcc14[35]].toLowerCase()==OxOcc14[59]){Ox2a6=Ox39a[OxOcc14[56]][Ox39b];} ;} ;var Ox39c=ts_getInnerText(Ox2a6);var Ox1e4=Ox39a[OxOcc14[17]];var Ox39d=Ox1e4[OxOcc14[60]];var Ox395=getParent(Ox1e4,OxOcc14[61]);if(Ox395[OxOcc14[48]][OxOcc14[30]]<=1){return ;} ;var Ox39e=ts_getInnerText(Ox395[OxOcc14[48]][1][OxOcc14[49]][Ox39d]);var Ox39f=ts_sort_caseinsensitive;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^[?]/)){Ox39f=ts_sort_currency;} ;if(Ox39e.match(/^[\d\.]+$/)){Ox39f=ts_sort_numeric;} ;SORT_COLUMN_INDEX=Ox39d;var Ox396= new Array();var Ox3a0= new Array();for(var i=0;i<Ox395[OxOcc14[48]][0][OxOcc14[30]];i++){Ox396[i]=Ox395[OxOcc14[48]][0][i];} ;for(var Ox25=1;Ox25<Ox395[OxOcc14[48]][OxOcc14[30]];Ox25++){Ox3a0[Ox25-1]=Ox395[OxOcc14[48]][Ox25];} ;Ox3a0.sort(Ox39f);if(Ox2a6.getAttribute(OxOcc14[62])==OxOcc14[63]){var Ox3a1=OxOcc14[64];Ox3a0.reverse();Ox2a6.setAttribute(OxOcc14[62],OxOcc14[65]);} else {Ox3a1=OxOcc14[66];Ox2a6.setAttribute(OxOcc14[62],OxOcc14[63]);} ;for(i=0;i<Ox3a0[OxOcc14[30]];i++){if(!Ox3a0[i][OxOcc14[46]]||(Ox3a0[i][OxOcc14[46]]&&(Ox3a0[i][OxOcc14[46]].indexOf(OxOcc14[67])==-1))){Ox395[OxOcc14[68]][0].appendChild(Ox3a0[i]);} ;} ;for(i=0;i<Ox3a0[OxOcc14[30]];i++){if(Ox3a0[i][OxOcc14[46]]&&(Ox3a0[i][OxOcc14[46]].indexOf(OxOcc14[67])!=-1)){Ox395[OxOcc14[68]][0].appendChild(Ox3a0[i]);} ;} ;var Ox3a2=document.getElementsByTagName(OxOcc14[59]);for(var Ox39b=0;Ox39b<Ox3a2[OxOcc14[30]];Ox39b++){if(Ox3a2[Ox39b][OxOcc14[46]]==OxOcc14[69]){if(getParent(Ox3a2[Ox39b],OxOcc14[43])==getParent(Ox39a,OxOcc14[43])){Ox3a2[Ox39b][OxOcc14[50]]=OxOcc14[70];} ;} ;} ;Ox2a6[OxOcc14[50]]=Ox3a1;} ;function getParent(Ox29,Ox3a4){if(Ox29==null){return null;} else {if(Ox29[OxOcc14[58]]==1&&Ox29[OxOcc14[35]].toLowerCase()==Ox3a4.toLowerCase()){return Ox29;} else {return getParent(Ox29.parentNode,Ox3a4);} ;} ;} ;function ts_sort_date(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxOcc14[49]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxOcc14[49]][SORT_COLUMN_INDEX]);if(Ox3a6[OxOcc14[30]]==10){var Ox3a8=Ox3a6.substr(6,4)+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} else {var Ox3a9=Ox3a6.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxOcc14[71]+Ox3a9;} else {Ox3a9=OxOcc14[72]+Ox3a9;} ;var Ox3a8=Ox3a9+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} ;if(Ox3a7[OxOcc14[30]]==10){var Ox3aa=Ox3a7.substr(6,4)+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} else {Ox3a9=Ox3a7.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxOcc14[71]+Ox3a9;} else {Ox3a9=OxOcc14[72]+Ox3a9;} ;var Ox3aa=Ox3a9+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} ;if(Ox3a8==Ox3aa){return 0;} ;if(Ox3a8<Ox3aa){return -1;} ;return 1;} ;function ts_sort_currency(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxOcc14[49]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxOcc14[9]);var Ox3a7=ts_getInnerText(b[OxOcc14[49]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxOcc14[9]);return parseFloat(Ox3a6)-parseFloat(Ox3a7);} ;function ts_sort_numeric(Oxee,b){var Ox3a6=parseFloat(ts_getInnerText(Oxee[OxOcc14[49]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a6)){Ox3a6=0;} ;var Ox3a7=parseFloat(ts_getInnerText(b[OxOcc14[49]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a7)){Ox3a7=0;} ;return Ox3a6-Ox3a7;} ;function ts_sort_caseinsensitive(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxOcc14[49]][SORT_COLUMN_INDEX]).toLowerCase();var Ox3a7=ts_getInnerText(b[OxOcc14[49]][SORT_COLUMN_INDEX]).toLowerCase();if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} ;function ts_sort_default(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxOcc14[49]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxOcc14[49]][SORT_COLUMN_INDEX]);if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} ;function RequireFileBrowseScript(){} ;function Actualsize(){} ;RequireFileBrowseScript();var browse_Frame=Window_GetElement(window,OxOcc14[73],true);var hiddenDirectory=Window_GetElement(window,OxOcc14[0],true);var hiddenFile=Window_GetElement(window,OxOcc14[1],true);var hiddenAlert=Window_GetElement(window,OxOcc14[2],true);var hiddenAction=Window_GetElement(window,OxOcc14[3],true);var hiddenActionData=Window_GetElement(window,OxOcc14[4],true);var Image1=Window_GetElement(window,OxOcc14[74],true);var FolderDescription=Window_GetElement(window,OxOcc14[75],true);var CreateDir=Window_GetElement(window,OxOcc14[76],true);var Copy=Window_GetElement(window,OxOcc14[77],true);var Move=Window_GetElement(window,OxOcc14[78],true);var FoldersAndFiles=Window_GetElement(window,OxOcc14[28],true);var Delete=Window_GetElement(window,OxOcc14[79],true);var DoRefresh=Window_GetElement(window,OxOcc14[80],true);var divpreview=Window_GetElement(window,OxOcc14[81],true);var TargetUrl=Window_GetElement(window,OxOcc14[39],true);var Button1=Window_GetElement(window,OxOcc14[82],true);var Button2=Window_GetElement(window,OxOcc14[83],true);var btn_zoom_in=Window_GetElement(window,OxOcc14[84],true);var btn_zoom_out=Window_GetElement(window,OxOcc14[85],true);var btn_Actualsize=Window_GetElement(window,OxOcc14[86],true);var arg=Window_GetDialogArguments(window);var editor=arg[OxOcc14[87]];var editwin=arg[OxOcc14[88]];var editdoc=arg[OxOcc14[89]];var ver=getInternetExplorerVersion();if(ver>-1&&ver<=9.0){var needAdjust=true;if(ver>=8.0&&document[OxOcc14[90]]){if(document[OxOcc14[91]]>7){needAdjust=false;} ;} ;if(needAdjust&&(browse_Frame[OxOcc14[92]]<browse_Frame[OxOcc14[93]])){FoldersAndFiles[OxOcc14[95]][OxOcc14[94]]=OxOcc14[96];} ;} ;function getInternetExplorerVersion(){var Ox3ca=-1;if(navigator[OxOcc14[97]]==OxOcc14[98]){var Ox3cb=navigator[OxOcc14[99]];var Ox296= new RegExp(OxOcc14[100]);if(Ox296.exec(Ox3cb)!=null){Ox3ca=parseFloat(RegExp.$1);} ;} ;return Ox3ca;} ;do_preview();function do_preview(Ox283){if(Ox283!=OxOcc14[9]&&Ox283!=null){htmlcode=Ox283;divpreview[OxOcc14[50]]=Ox283;return ;} ;divpreview[OxOcc14[50]]=OxOcc14[9];var Ox288=TargetUrl[OxOcc14[10]];if(Ox288==OxOcc14[9]){return ;} ;var Oxca=Ox288.substring(Ox288.lastIndexOf(OxOcc14[21])).toLowerCase();switch(Oxca){case OxOcc14[101]:;case OxOcc14[102]:;case OxOcc14[103]:;case OxOcc14[104]:;case OxOcc14[107]:divpreview[OxOcc14[50]]=OxOcc14[105]+Ox288+OxOcc14[106];break ;;case OxOcc14[110]:var Ox3cc=OxOcc14[108]+Ox288+OxOcc14[109];divpreview[OxOcc14[50]]=Ox3cc+OxOcc14[70];break ;;case OxOcc14[111]:;case OxOcc14[112]:;case OxOcc14[113]:;case OxOcc14[116]:var Ox3cd=OxOcc14[114]+Ox288+OxOcc14[115];divpreview[OxOcc14[50]]=Ox3cd+OxOcc14[70];break ;;} ;} ;function do_insert(){var Ox473=arg[OxOcc14[117]];if(Ox473){try{Ox473[OxOcc14[10]]=TargetUrl[OxOcc14[10]];} catch(x){} ;} ;Window_SetDialogReturnValue(window,TargetUrl.value);Window_CloseDialog(window);} ;function do_Close(){Window_SetDialogReturnValue(window,null);Window_CloseDialog(window);} ;function Zoom_In(){if(divpreview[OxOcc14[95]][OxOcc14[118]]!=0){divpreview[OxOcc14[95]][OxOcc14[118]]*=1.2;} else {divpreview[OxOcc14[95]][OxOcc14[118]]=1.2;} ;} ;function Zoom_Out(){if(divpreview[OxOcc14[95]][OxOcc14[118]]!=0){divpreview[OxOcc14[95]][OxOcc14[118]]*=0.8;} else {divpreview[OxOcc14[95]][OxOcc14[118]]=0.8;} ;} ;function Actualsize(){divpreview[OxOcc14[95]][OxOcc14[118]]=1;do_preview();} ;if(!Browser_IsWinIE()){btn_zoom_in[OxOcc14[95]][OxOcc14[119]]=btn_zoom_out[OxOcc14[95]][OxOcc14[119]]=btn_Actualsize[OxOcc14[95]][OxOcc14[119]]=OxOcc14[120];} else {} ;if(Browser_IsIE7()){var _dialogPromptID=null;function IEprompt(Ox221,Ox222,Ox223){that=this;this[OxOcc14[121]]=function (Ox224){val=document.getElementById(OxOcc14[122])[OxOcc14[10]];_dialogPromptID[OxOcc14[95]][OxOcc14[119]]=OxOcc14[120];document.getElementById(OxOcc14[122])[OxOcc14[10]]=OxOcc14[9];if(Ox224){val=OxOcc14[9];} ;Ox221(val);return false;} ;if(Ox223==undefined){Ox223=OxOcc14[9];} ;if(_dialogPromptID==null){var Ox225=document.getElementsByTagName(OxOcc14[123])[0];tnode=document.createElement(OxOcc14[124]);tnode[OxOcc14[47]]=OxOcc14[125];Ox225.appendChild(tnode);_dialogPromptID=document.getElementById(OxOcc14[125]);tnode=document.createElement(OxOcc14[124]);tnode[OxOcc14[47]]=OxOcc14[126];Ox225.appendChild(tnode);_dialogPromptID[OxOcc14[95]][OxOcc14[127]]=OxOcc14[128];_dialogPromptID[OxOcc14[95]][OxOcc14[129]]=OxOcc14[130];_dialogPromptID[OxOcc14[95]][OxOcc14[131]]=OxOcc14[132];_dialogPromptID[OxOcc14[95]][OxOcc14[94]]=OxOcc14[133];_dialogPromptID[OxOcc14[95]][OxOcc14[134]]=OxOcc14[135];} ;var Ox226=OxOcc14[136];Ox226+=OxOcc14[137]+Ox222+OxOcc14[138];Ox226+=OxOcc14[139];Ox226+=OxOcc14[140]+Ox223+OxOcc14[141];Ox226+=OxOcc14[142];Ox226+=OxOcc14[143];Ox226+=OxOcc14[144];Ox226+=OxOcc14[145];Ox226+=OxOcc14[146];_dialogPromptID[OxOcc14[50]]=Ox226;_dialogPromptID[OxOcc14[95]][OxOcc14[147]]=OxOcc14[148];_dialogPromptID[OxOcc14[95]][OxOcc14[149]]=parseInt((document[OxOcc14[123]][OxOcc14[150]]-315)/2)+OxOcc14[151];_dialogPromptID[OxOcc14[95]][OxOcc14[119]]=OxOcc14[152];var Ox227=document.getElementById(OxOcc14[122]);try{var Ox228=Ox227.createTextRange();Ox228.collapse(false);Ox228.select();} catch(x){Ox227.focus();} ;} ;} ;if(CreateDir){CreateDir[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(Copy){Copy[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(Move){Move[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(Delete){Delete[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(DoRefresh){DoRefresh[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(btn_zoom_in){btn_zoom_in[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(btn_zoom_out){btn_zoom_out[OxOcc14[31]]= new Function(OxOcc14[153]);} ;if(btn_Actualsize){btn_Actualsize[OxOcc14[31]]= new Function(OxOcc14[153]);} ;