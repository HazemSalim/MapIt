var OxObad9=["hiddenDirectory","hiddenFile","hiddenAlert","hiddenAction","hiddenActionData","This function is disabled in the demo mode.","disabled","[[Disabled]]","[[SpecifyNewFolderName]]","","value","createdir","[[CopyMoveto]]","/","move","copy","[[AreyouSureDelete]]","parentNode","text","isdir","true",".","[[SpecifyNewFileName]]","rename","True","False",":","path","FoldersAndFiles","TR","length","onmouseover","this.style.backgroundColor=\x27#eeeeee\x27;","onmouseout","this.style.backgroundColor=\x27\x27;","nodeName","INPUT","changedir","url","TargetUrl","htmlcode","onload","getElementsByTagName","table","sortable"," ","className","id","rows","cells","innerHTML","\x3Ca href=\x22#\x22 onclick=\x22ts_resortTable(this);return false;\x22\x3E","\x3Cspan class=\x22sortarrow\x22\x3E\x26nbsp;\x3C/span\x3E\x3C/a\x3E","string","undefined","innerText","childNodes","nodeValue","nodeType","span","cellIndex","TABLE","sortdir","down","\x26uarr;","up","\x26darr;","sortbottom","tBodies","sortarrow","\x26nbsp;","20","19","browse_Frame","Image1","FolderDescription","CreateDir","Copy","Move","Delete","DoRefresh","name_Cell","size_Cell","op_Cell","row0","row0_cb","divpreview","Width","Height","AutoStart","ShowControls","ShowStatusBar","WindowlessVideo","Button1","Button2","btn_zoom_in","btn_zoom_out","btn_Actualsize","documentElement","documentMode","clientHeight","scrollHeight","width","style","235px","appName","Microsoft Internet Explorer","userAgent","MSIE ([0-9]{1,}[.0-9]{0,})","checked","\x3Cembed name=\x22MediaPlayer1\x22 src=\x22","\x22 autostart=\x22","\x22 showcontrols=\x22","\x22  windowlessvideo=\x22","\x22 showstatusbar=\x22","\x22 width=\x22","\x22 height=\x22","\x22 type=\x22application/x-mplayer2\x22 pluginspage=\x22http://www.microsoft.com/Windows/MediaPlayer\x22 \x3E\x3C/embed\x3E\x0A","\x3Cobject classid=\x22CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95\x22 "," codebase=\x22http://activex.microsoft.com/activex/"," controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902\x22 "," standby=\x22Loading Microsoft Windows Media Player components...\x22 "," type=\x22application/x-oleobject\x22","  height=\x22","\x22 \x3E","\x3Cparam name=\x22FileName\x22 value=\x22","\x22/\x3E","\x3Cparam name=\x22autoStart\x22 value=\x22","\x3Cparam name=\x22showControls\x22 value=\x22","\x3Cparam name=\x22showstatusbar\x22 value=\x22","\x3Cparam name=\x22windowlessvideo\x22 value=\x22","\x3C/object\x3E","onbeforeunload","onunload","Please choose a Media movie to insert","\x22 windowlessvideo=\x22","zoom","wrapupPrompt","iepromptfield","display","none","body","div","IEPromptBox","promptBlackout","border","1px solid #b0bec7","backgroundColor","#f0f0f0","position","absolute","330px","zIndex","100","\x3Cdiv style=\x22width: 100%; padding-top:3px;background-color: #DCE7EB; font-family: verdana; font-size: 10pt; font-weight: bold; height: 22px; text-align:center; background:url(Load.ashx?type=image\x26file=formbg2.gif) repeat-x left top;\x22\x3E[[InputRequired]]\x3C/div\x3E","\x3Cdiv style=\x22padding: 10px\x22\x3E","\x3CBR\x3E\x3CBR\x3E","\x3Cform action=\x22\x22 onsubmit=\x22return wrapupPrompt()\x22\x3E","\x3Cinput id=\x22iepromptfield\x22 name=\x22iepromptdata\x22 type=text size=46 value=\x22","\x22\x3E","\x3Cbr\x3E\x3Cbr\x3E\x3Ccenter\x3E","\x3Cinput type=\x22submit\x22 value=\x22\x26nbsp;\x26nbsp;\x26nbsp;[[OK]]\x26nbsp;\x26nbsp;\x26nbsp;\x22\x3E","\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;","\x3Cinput type=\x22button\x22 onclick=\x22wrapupPrompt(true)\x22 value=\x22\x26nbsp;[[Cancel]]\x26nbsp;\x22\x3E","\x3C/form\x3E\x3C/div\x3E","top","100px","left","offsetWidth","px","block","CuteEditor_ColorPicker_ButtonOver(this)"];var hiddenDirectory=Window_GetElement(window,OxObad9[0],true);var hiddenFile=Window_GetElement(window,OxObad9[1],true);var hiddenAlert=Window_GetElement(window,OxObad9[2],true);var hiddenAction=Window_GetElement(window,OxObad9[3],true);var hiddenActionData=Window_GetElement(window,OxObad9[4],true);function CreateDir_click(){if(isDemoMode){alert(OxObad9[5]);return false;} ;if(Event_GetSrcElement()[OxObad9[6]]){alert(OxObad9[7]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxObad9[8],OxObad9[9]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxObad9[10]]=Ox382;hiddenAction[OxObad9[10]]=OxObad9[11];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxObad9[8],OxObad9[9]);if(Ox382){hiddenActionData[OxObad9[10]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Move_click(){if(isDemoMode){alert(OxObad9[5]);return false;} ;if(Event_GetSrcElement()[OxObad9[6]]){alert(OxObad9[7]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxObad9[12],OxObad9[13]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxObad9[10]]=Ox382;hiddenAction[OxObad9[10]]=OxObad9[14];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxObad9[12],OxObad9[13]);if(Ox382){hiddenActionData[OxObad9[10]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Copy_click(){if(isDemoMode){alert(OxObad9[5]);return false;} ;if(Event_GetSrcElement()[OxObad9[6]]){alert(OxObad9[7]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxObad9[12],OxObad9[13]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxObad9[10]]=Ox382;hiddenAction[OxObad9[10]]=OxObad9[15];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxObad9[12],OxObad9[13]);if(Ox382){hiddenActionData[OxObad9[10]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Delete_click(){if(isDemoMode){alert(OxObad9[5]);return false;} ;if(Event_GetSrcElement()[OxObad9[6]]){alert(OxObad9[7]);return false;} ;return confirm(OxObad9[16]);} ;function EditImg_click(img){if(isDemoMode){alert(OxObad9[5]);return false;} ;if(img[OxObad9[6]]){alert(OxObad9[7]);return false;} ;var Ox387=img[OxObad9[17]][OxObad9[17]];var Ox388=Ox387.getAttribute(OxObad9[18]);var name;var Ox389;Ox389=Ox387.getAttribute(OxObad9[19])==OxObad9[20];if(Browser_IsIE7()){var Oxca;if(Ox389){IEprompt(Ox221,OxObad9[8],Ox388);} else {var i=Ox388.lastIndexOf(OxObad9[21]);Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxObad9[21]));IEprompt(Ox221,OxObad9[22],Ox12);} ;function Ox221(Ox382){if(Ox382&&Ox382!=Ox387.getAttribute(OxObad9[18])){if(!Ox389){Ox382=Ox382+Oxca;} ;hiddenAction[OxObad9[10]]=OxObad9[23];hiddenActionData[OxObad9[10]]=(Ox389?OxObad9[24]:OxObad9[25])+OxObad9[26]+Ox387.getAttribute(OxObad9[27])+OxObad9[26]+Ox382;window.PostBackAction();} ;} ;} else {if(Ox389){name=prompt(OxObad9[8],Ox388);} else {var i=Ox388.lastIndexOf(OxObad9[21]);var Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxObad9[21]));name=prompt(OxObad9[22],Ox12);if(name){name=name+Oxca;} ;} ;if(name&&name!=Ox387.getAttribute(OxObad9[18])){hiddenAction[OxObad9[10]]=OxObad9[23];hiddenActionData[OxObad9[10]]=(Ox389?OxObad9[24]:OxObad9[25])+OxObad9[26]+Ox387.getAttribute(OxObad9[27])+OxObad9[26]+name;window.PostBackAction();} ;} ;return Event_CancelEvent();} ;setMouseOver();function setMouseOver(){var FoldersAndFiles=Window_GetElement(window,OxObad9[28],true);var Ox38c=FoldersAndFiles.getElementsByTagName(OxObad9[29]);for(var i=1;i<Ox38c[OxObad9[30]];i++){var Ox387=Ox38c[i];Ox387[OxObad9[31]]= new Function(OxObad9[9],OxObad9[32]);Ox387[OxObad9[33]]= new Function(OxObad9[9],OxObad9[34]);} ;} ;function row_click(Ox387){var Ox389;Ox389=Ox387.getAttribute(OxObad9[19])==OxObad9[20];if(Ox389){if(Event_GetSrcElement()[OxObad9[35]]==OxObad9[36]){return ;} ;hiddenAction[OxObad9[10]]=OxObad9[37];hiddenActionData[OxObad9[10]]=Ox387.getAttribute(OxObad9[27]);window.PostBackAction();} else {var Ox109=Ox387.getAttribute(OxObad9[27]);hiddenFile[OxObad9[10]]=Ox109;var Ox288=Ox387.getAttribute(OxObad9[38]);Window_GetElement(window,OxObad9[39],true)[OxObad9[10]]=Ox288;var htmlcode=Ox387.getAttribute(OxObad9[40]);if(htmlcode!=OxObad9[9]&&htmlcode!=null){do_preview(htmlcode);} else {try{Actualsize();} catch(x){do_preview();} ;} ;} ;} ;function do_preview(){} ;function reset_hiddens(){if(hiddenAlert[OxObad9[10]]){alert(hiddenAlert.value);} ;hiddenAlert[OxObad9[10]]=OxObad9[9];hiddenAction[OxObad9[10]]=OxObad9[9];hiddenActionData[OxObad9[10]]=OxObad9[9];} ;Event_Attach(window,OxObad9[41],reset_hiddens);function RequireFileBrowseScript(){} ;function Actualsize(){} ;Event_Attach(window,OxObad9[41],sortables_init);var SORT_COLUMN_INDEX;function sortables_init(){if(!document[OxObad9[42]]){return ;} ;var Ox391=document.getElementsByTagName(OxObad9[43]);for(var Ox392=0;Ox392<Ox391[OxObad9[30]];Ox392++){var Ox393=Ox391[Ox392];if(((OxObad9[45]+Ox393[OxObad9[46]]+OxObad9[45]).indexOf(OxObad9[44])!=-1)&&(Ox393[OxObad9[47]])){ts_makeSortable(Ox393);} ;} ;} ;function ts_makeSortable(Ox395){if(Ox395[OxObad9[48]]&&Ox395[OxObad9[48]][OxObad9[30]]>0){var Ox396=Ox395[OxObad9[48]][0];} ;if(!Ox396){return ;} ;for(var i=2;i<4;i++){var Ox397=Ox396[OxObad9[49]][i];var Ox219=ts_getInnerText(Ox397);Ox397[OxObad9[50]]=OxObad9[51]+Ox219+OxObad9[52];} ;} ;function ts_getInnerText(Ox29){if( typeof Ox29==OxObad9[53]){return Ox29;} ;if( typeof Ox29==OxObad9[54]){return Ox29;} ;if(Ox29[OxObad9[55]]){return Ox29[OxObad9[55]];} ;var Ox24=OxObad9[9];var Ox343=Ox29[OxObad9[56]];var Ox11=Ox343[OxObad9[30]];for(var i=0;i<Ox11;i++){switch(Ox343[i][OxObad9[58]]){case 1:Ox24+=ts_getInnerText(Ox343[i]);break ;;case 3:Ox24+=Ox343[i][OxObad9[57]];break ;;} ;} ;return Ox24;} ;function ts_resortTable(Ox39a){var Ox2a6;for(var Ox39b=0;Ox39b<Ox39a[OxObad9[56]][OxObad9[30]];Ox39b++){if(Ox39a[OxObad9[56]][Ox39b][OxObad9[35]]&&Ox39a[OxObad9[56]][Ox39b][OxObad9[35]].toLowerCase()==OxObad9[59]){Ox2a6=Ox39a[OxObad9[56]][Ox39b];} ;} ;var Ox39c=ts_getInnerText(Ox2a6);var Ox1e4=Ox39a[OxObad9[17]];var Ox39d=Ox1e4[OxObad9[60]];var Ox395=getParent(Ox1e4,OxObad9[61]);if(Ox395[OxObad9[48]][OxObad9[30]]<=1){return ;} ;var Ox39e=ts_getInnerText(Ox395[OxObad9[48]][1][OxObad9[49]][Ox39d]);var Ox39f=ts_sort_caseinsensitive;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^[?]/)){Ox39f=ts_sort_currency;} ;if(Ox39e.match(/^[\d\.]+$/)){Ox39f=ts_sort_numeric;} ;SORT_COLUMN_INDEX=Ox39d;var Ox396= new Array();var Ox3a0= new Array();for(var i=0;i<Ox395[OxObad9[48]][0][OxObad9[30]];i++){Ox396[i]=Ox395[OxObad9[48]][0][i];} ;for(var Ox25=1;Ox25<Ox395[OxObad9[48]][OxObad9[30]];Ox25++){Ox3a0[Ox25-1]=Ox395[OxObad9[48]][Ox25];} ;Ox3a0.sort(Ox39f);if(Ox2a6.getAttribute(OxObad9[62])==OxObad9[63]){var Ox3a1=OxObad9[64];Ox3a0.reverse();Ox2a6.setAttribute(OxObad9[62],OxObad9[65]);} else {Ox3a1=OxObad9[66];Ox2a6.setAttribute(OxObad9[62],OxObad9[63]);} ;for(i=0;i<Ox3a0[OxObad9[30]];i++){if(!Ox3a0[i][OxObad9[46]]||(Ox3a0[i][OxObad9[46]]&&(Ox3a0[i][OxObad9[46]].indexOf(OxObad9[67])==-1))){Ox395[OxObad9[68]][0].appendChild(Ox3a0[i]);} ;} ;for(i=0;i<Ox3a0[OxObad9[30]];i++){if(Ox3a0[i][OxObad9[46]]&&(Ox3a0[i][OxObad9[46]].indexOf(OxObad9[67])!=-1)){Ox395[OxObad9[68]][0].appendChild(Ox3a0[i]);} ;} ;var Ox3a2=document.getElementsByTagName(OxObad9[59]);for(var Ox39b=0;Ox39b<Ox3a2[OxObad9[30]];Ox39b++){if(Ox3a2[Ox39b][OxObad9[46]]==OxObad9[69]){if(getParent(Ox3a2[Ox39b],OxObad9[43])==getParent(Ox39a,OxObad9[43])){Ox3a2[Ox39b][OxObad9[50]]=OxObad9[70];} ;} ;} ;Ox2a6[OxObad9[50]]=Ox3a1;} ;function getParent(Ox29,Ox3a4){if(Ox29==null){return null;} else {if(Ox29[OxObad9[58]]==1&&Ox29[OxObad9[35]].toLowerCase()==Ox3a4.toLowerCase()){return Ox29;} else {return getParent(Ox29.parentNode,Ox3a4);} ;} ;} ;function ts_sort_date(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxObad9[49]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxObad9[49]][SORT_COLUMN_INDEX]);if(Ox3a6[OxObad9[30]]==10){var Ox3a8=Ox3a6.substr(6,4)+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} else {var Ox3a9=Ox3a6.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxObad9[71]+Ox3a9;} else {Ox3a9=OxObad9[72]+Ox3a9;} ;var Ox3a8=Ox3a9+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} ;if(Ox3a7[OxObad9[30]]==10){var Ox3aa=Ox3a7.substr(6,4)+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} else {Ox3a9=Ox3a7.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxObad9[71]+Ox3a9;} else {Ox3a9=OxObad9[72]+Ox3a9;} ;var Ox3aa=Ox3a9+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} ;if(Ox3a8==Ox3aa){return 0;} ;if(Ox3a8<Ox3aa){return -1;} ;return 1;} ;function ts_sort_currency(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxObad9[49]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxObad9[9]);var Ox3a7=ts_getInnerText(b[OxObad9[49]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxObad9[9]);return parseFloat(Ox3a6)-parseFloat(Ox3a7);} ;function ts_sort_numeric(Oxee,b){var Ox3a6=parseFloat(ts_getInnerText(Oxee[OxObad9[49]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a6)){Ox3a6=0;} ;var Ox3a7=parseFloat(ts_getInnerText(b[OxObad9[49]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a7)){Ox3a7=0;} ;return Ox3a6-Ox3a7;} ;function ts_sort_caseinsensitive(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxObad9[49]][SORT_COLUMN_INDEX]).toLowerCase();var Ox3a7=ts_getInnerText(b[OxObad9[49]][SORT_COLUMN_INDEX]).toLowerCase();if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} ;function ts_sort_default(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxObad9[49]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxObad9[49]][SORT_COLUMN_INDEX]);if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} [sortables_init];RequireFileBrowseScript();var browse_Frame=Window_GetElement(window,OxObad9[73],true);var hiddenDirectory=Window_GetElement(window,OxObad9[0],true);var hiddenFile=Window_GetElement(window,OxObad9[1],true);var hiddenAlert=Window_GetElement(window,OxObad9[2],true);var hiddenAction=Window_GetElement(window,OxObad9[3],true);var hiddenActionData=Window_GetElement(window,OxObad9[4],true);var Image1=Window_GetElement(window,OxObad9[74],true);var FolderDescription=Window_GetElement(window,OxObad9[75],true);var CreateDir=Window_GetElement(window,OxObad9[76],true);var Copy=Window_GetElement(window,OxObad9[77],true);var Move=Window_GetElement(window,OxObad9[78],true);var FoldersAndFiles=Window_GetElement(window,OxObad9[28],true);var Delete=Window_GetElement(window,OxObad9[79],true);var DoRefresh=Window_GetElement(window,OxObad9[80],true);var name_Cell=Window_GetElement(window,OxObad9[81],true);var size_Cell=Window_GetElement(window,OxObad9[82],true);var op_Cell=Window_GetElement(window,OxObad9[83],true);var row0=Window_GetElement(window,OxObad9[84],true);var row0_cb=Window_GetElement(window,OxObad9[85],true);var divpreview=Window_GetElement(window,OxObad9[86],true);var Width=Window_GetElement(window,OxObad9[87],true);var Height=Window_GetElement(window,OxObad9[88],true);var AutoStart=Window_GetElement(window,OxObad9[89],true);var ShowControls=Window_GetElement(window,OxObad9[90],true);var ShowStatusBar=Window_GetElement(window,OxObad9[91],true);var WindowlessVideo=Window_GetElement(window,OxObad9[92],true);var TargetUrl=Window_GetElement(window,OxObad9[39],true);var Button1=Window_GetElement(window,OxObad9[93],true);var Button2=Window_GetElement(window,OxObad9[94],true);var btn_zoom_in=Window_GetElement(window,OxObad9[95],true);var btn_zoom_out=Window_GetElement(window,OxObad9[96],true);var btn_Actualsize=Window_GetElement(window,OxObad9[97],true);var editor=Window_GetDialogArguments(window);var editor=Window_GetDialogArguments(window);var ver=getInternetExplorerVersion();if(ver>-1&&ver<=9.0){var needAdjust=true;if(ver>=8.0&&document[OxObad9[98]]){if(document[OxObad9[99]]>7){needAdjust=false;} ;} ;if(needAdjust&&(browse_Frame[OxObad9[100]]<browse_Frame[OxObad9[101]])){FoldersAndFiles[OxObad9[103]][OxObad9[102]]=OxObad9[104];} ;} ;function getInternetExplorerVersion(){var Ox3ca=-1;if(navigator[OxObad9[105]]==OxObad9[106]){var Ox3cb=navigator[OxObad9[107]];var Ox296= new RegExp(OxObad9[108]);if(Ox296.exec(Ox3cb)!=null){Ox3ca=parseFloat(RegExp.$1);} ;} ;return Ox3ca;} ;do_preview();function do_preview(){var Ox433;var Ox74;var Ox73;if(TargetUrl[OxObad9[10]]==OxObad9[9]){return ;} ;var Ox434,Ox435,Ox436,Ox437;if(AutoStart[OxObad9[109]]){Ox434=1;} else {Ox434=0;} ;if(ShowStatusBar[OxObad9[109]]){Ox435=1;} else {Ox435=0;} ;if(ShowControls[OxObad9[109]]){Ox436=1;} else {Ox436=0;} ;if(WindowlessVideo[OxObad9[109]]){Ox437=true;} else {Ox437=false;} ;Ox74=Width[OxObad9[10]];Ox73=Height[OxObad9[10]];Ox74=parseInt(Ox74);Ox73=parseInt(Ox73);var Ox3f0=OxObad9[110]+TargetUrl[OxObad9[10]]+OxObad9[111]+Ox434+OxObad9[112]+Ox436+OxObad9[113]+Ox437+OxObad9[114]+Ox435+OxObad9[115]+Ox74+OxObad9[116]+Ox73+OxObad9[117];var Ox3cd=OxObad9[118]+OxObad9[119]+OxObad9[120]+OxObad9[121]+OxObad9[122]+OxObad9[123]+Ox73+OxObad9[115]+Ox74+OxObad9[124];Ox3cd=Ox3cd+OxObad9[125]+TargetUrl[OxObad9[10]]+OxObad9[126];Ox3cd=Ox3cd+OxObad9[127]+Ox434+OxObad9[126];Ox3cd=Ox3cd+OxObad9[128]+Ox436+OxObad9[126];Ox3cd=Ox3cd+OxObad9[129]+Ox435+OxObad9[126];Ox3cd=Ox3cd+OxObad9[130]+Ox437+OxObad9[126];Ox3cd=Ox3cd+Ox3f0+OxObad9[131];Ox3f0=Ox3cd;divpreview[OxObad9[50]]=Ox3f0;} ;window[OxObad9[132]]=window[OxObad9[133]]=function (){divpreview[OxObad9[50]]=OxObad9[9];} ;var parameters= new Array();function do_insert(){divpreview[OxObad9[50]]=OxObad9[9];if(TargetUrl[OxObad9[10]]==OxObad9[9]){alert(OxObad9[134]);return false;} ;var Ox434,Ox435,Ox436,Ox437;if(AutoStart[OxObad9[109]]){Ox434=1;} else {Ox434=0;} ;if(ShowStatusBar[OxObad9[109]]){Ox435=1;} else {Ox435=0;} ;if(ShowControls[OxObad9[109]]){Ox436=1;} else {Ox436=0;} ;if(WindowlessVideo[OxObad9[109]]){Ox437=true;} else {Ox437=false;} ;width=Width[OxObad9[10]]+OxObad9[9];height=Height[OxObad9[10]]+OxObad9[9];width=parseInt(width);height=parseInt(height);var Ox3f0=OxObad9[110]+TargetUrl[OxObad9[10]]+OxObad9[111]+Ox434+OxObad9[112]+Ox436+OxObad9[114]+Ox435+OxObad9[135]+Ox437+OxObad9[115]+width+OxObad9[116]+height+OxObad9[117];var Ox3cd=OxObad9[118]+OxObad9[119]+OxObad9[120]+OxObad9[121]+OxObad9[122]+OxObad9[123]+height+OxObad9[115]+width+OxObad9[124];Ox3cd=Ox3cd+OxObad9[125]+TargetUrl[OxObad9[10]]+OxObad9[126];Ox3cd=Ox3cd+OxObad9[127]+Ox434+OxObad9[126];Ox3cd=Ox3cd+OxObad9[128]+Ox436+OxObad9[126];Ox3cd=Ox3cd+OxObad9[129]+Ox435+OxObad9[126];Ox3cd=Ox3cd+OxObad9[130]+Ox437+OxObad9[126];Ox3cd=Ox3cd+Ox3f0+OxObad9[131];Ox3f0=Ox3cd;editor.PasteHTML(Ox3f0);Window_CloseDialog(window);} ;function do_Close(){divpreview[OxObad9[50]]=OxObad9[9];Window_CloseDialog(window);} ;function Zoom_In(){if(divpreview[OxObad9[103]][OxObad9[136]]!=0){divpreview[OxObad9[103]][OxObad9[136]]*=1.2;} else {divpreview[OxObad9[103]][OxObad9[136]]=1.2;} ;} ;function Zoom_Out(){if(divpreview[OxObad9[103]][OxObad9[136]]!=0){divpreview[OxObad9[103]][OxObad9[136]]*=0.8;} else {divpreview[OxObad9[103]][OxObad9[136]]=0.8;} ;} ;function Actualsize(){divpreview[OxObad9[103]][OxObad9[136]]=1;do_preview();} ;if(Browser_IsIE7()){var _dialogPromptID=null;function IEprompt(Ox221,Ox222,Ox223){that=this;this[OxObad9[137]]=function (Ox224){val=document.getElementById(OxObad9[138])[OxObad9[10]];_dialogPromptID[OxObad9[103]][OxObad9[139]]=OxObad9[140];document.getElementById(OxObad9[138])[OxObad9[10]]=OxObad9[9];if(Ox224){val=OxObad9[9];} ;Ox221(val);return false;} ;if(Ox223==undefined){Ox223=OxObad9[9];} ;if(_dialogPromptID==null){var Ox225=document.getElementsByTagName(OxObad9[141])[0];tnode=document.createElement(OxObad9[142]);tnode[OxObad9[47]]=OxObad9[143];Ox225.appendChild(tnode);_dialogPromptID=document.getElementById(OxObad9[143]);tnode=document.createElement(OxObad9[142]);tnode[OxObad9[47]]=OxObad9[144];Ox225.appendChild(tnode);_dialogPromptID[OxObad9[103]][OxObad9[145]]=OxObad9[146];_dialogPromptID[OxObad9[103]][OxObad9[147]]=OxObad9[148];_dialogPromptID[OxObad9[103]][OxObad9[149]]=OxObad9[150];_dialogPromptID[OxObad9[103]][OxObad9[102]]=OxObad9[151];_dialogPromptID[OxObad9[103]][OxObad9[152]]=OxObad9[153];} ;var Ox226=OxObad9[154];Ox226+=OxObad9[155]+Ox222+OxObad9[156];Ox226+=OxObad9[157];Ox226+=OxObad9[158]+Ox223+OxObad9[159];Ox226+=OxObad9[160];Ox226+=OxObad9[161];Ox226+=OxObad9[162];Ox226+=OxObad9[163];Ox226+=OxObad9[164];_dialogPromptID[OxObad9[50]]=Ox226;_dialogPromptID[OxObad9[103]][OxObad9[165]]=OxObad9[166];_dialogPromptID[OxObad9[103]][OxObad9[167]]=parseInt((document[OxObad9[141]][OxObad9[168]]-315)/2)+OxObad9[169];_dialogPromptID[OxObad9[103]][OxObad9[139]]=OxObad9[170];var Ox227=document.getElementById(OxObad9[138]);try{var Ox228=Ox227.createTextRange();Ox228.collapse(false);Ox228.select();} catch(x){Ox227.focus();} ;} ;} ;if(!Browser_IsWinIE()){btn_zoom_in[OxObad9[103]][OxObad9[139]]=btn_zoom_out[OxObad9[103]][OxObad9[139]]=btn_Actualsize[OxObad9[103]][OxObad9[139]]=OxObad9[140];} else {} ;if(CreateDir){CreateDir[OxObad9[31]]= new Function(OxObad9[171]);} ;if(Copy){Copy[OxObad9[31]]= new Function(OxObad9[171]);} ;if(Move){Move[OxObad9[31]]= new Function(OxObad9[171]);} ;if(Delete){Delete[OxObad9[31]]= new Function(OxObad9[171]);} ;if(DoRefresh){DoRefresh[OxObad9[31]]= new Function(OxObad9[171]);} ;if(btn_zoom_in){btn_zoom_in[OxObad9[31]]= new Function(OxObad9[171]);} ;if(btn_zoom_out){btn_zoom_out[OxObad9[31]]= new Function(OxObad9[171]);} ;if(btn_Actualsize){btn_Actualsize[OxObad9[31]]= new Function(OxObad9[171]);} ;