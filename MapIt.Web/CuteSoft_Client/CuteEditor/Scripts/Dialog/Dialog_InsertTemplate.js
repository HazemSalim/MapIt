var OxO5962=["hiddenDirectory","hiddenFile","hiddenAlert","hiddenAction","hiddenActionData","hiddenHTML","This function is disabled in the demo mode.","disabled","[[Disabled]]","[[SpecifyNewFolderName]]","","value","createdir","[[CopyMoveto]]","/","move","copy","[[AreyouSureDelete]]","parentNode","path","[[TemplateModified]]","OuterEditorFull.aspx?","\x26f=","dialogWidth:772px;dialogHeight:385px;help:no;scroll:no;status:no;resizable:0;","UseStandardDialog","1","\x26Dialog=Standard","setting=","EditorSetting","\x26Theme=","Theme","\x26","DNNArg","[[TemplateCreated]]","refresh","[[SpecifyNewFileName]]","innerHTML","dialogWidth:760px;dialogHeight:385px;help:no;scroll:no;status:no;resizable:0;","isdir","true","text",".","rename","True","False",":","FoldersAndFiles","TR","length","onmouseover","this.style.backgroundColor=\x27#eeeeee\x27;","onmouseout","this.style.backgroundColor=\x27\x27;","nodeName","INPUT","changedir","url","TargetUrl","htmlcode","onload","getElementsByTagName","table","sortable"," ","className","id","rows","cells","\x3Ca href=\x22#\x22 onclick=\x22ts_resortTable(this);return false;\x22\x3E","\x3Cspan class=\x22sortarrow\x22\x3E\x26nbsp;\x3C/span\x3E\x3C/a\x3E","string","undefined","innerText","childNodes","nodeValue","nodeType","span","cellIndex","TABLE","sortdir","down","\x26uarr;","up","\x26darr;","sortbottom","tBodies","sortarrow","\x26nbsp;","20","19","browse_Frame","Image1","FolderDescription","CreateDir","Copy","Move","NewTemplate","Delete","DoRefresh","contentWindow","framepreview","btn_zoom_in","btn_zoom_out","btn_Actualsize","editor","documentElement","documentMode","clientHeight","scrollHeight","width","style","225px","appName","Microsoft Internet Explorer","userAgent","MSIE ([0-9]{1,}[.0-9]{0,})","?","src","body","document","\x26#",";","zoom","wrapupPrompt","iepromptfield","display","none","div","IEPromptBox","promptBlackout","border","1px solid #b0bec7","backgroundColor","#f0f0f0","position","absolute","330px","zIndex","100","\x3Cdiv style=\x22width: 100%; padding-top:3px;background-color: #DCE7EB; font-family: verdana; font-size: 10pt; font-weight: bold; height: 22px; text-align:center; background:url(Load.ashx?type=image\x26file=formbg2.gif) repeat-x left top;\x22\x3E[[InputRequired]]\x3C/div\x3E","\x3Cdiv style=\x22padding: 10px\x22\x3E","\x3CBR\x3E\x3CBR\x3E","\x3Cform action=\x22\x22 onsubmit=\x22return wrapupPrompt()\x22\x3E","\x3Cinput id=\x22iepromptfield\x22 name=\x22iepromptdata\x22 type=text size=46 value=\x22","\x22\x3E","\x3Cbr\x3E\x3Cbr\x3E\x3Ccenter\x3E","\x3Cinput type=\x22submit\x22 value=\x22\x26nbsp;\x26nbsp;\x26nbsp;[[OK]]\x26nbsp;\x26nbsp;\x26nbsp;\x22\x3E","\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;","\x3Cinput type=\x22button\x22 onclick=\x22wrapupPrompt(true)\x22 value=\x22\x26nbsp;[[Cancel]]\x26nbsp;\x22\x3E","\x3C/form\x3E\x3C/div\x3E","top","100px","left","offsetWidth","px","block","CuteEditor_ColorPicker_ButtonOver(this)"];var hiddenDirectory=Window_GetElement(window,OxO5962[0],true);var hiddenFile=Window_GetElement(window,OxO5962[1],true);var hiddenAlert=Window_GetElement(window,OxO5962[2],true);var hiddenAction=Window_GetElement(window,OxO5962[3],true);var hiddenActionData=Window_GetElement(window,OxO5962[4],true);var hiddenHTML=Window_GetElement(window,OxO5962[5],true);function CreateDir_click(){if(isDemoMode){alert(OxO5962[6]);return false;} ;if(Event_GetSrcElement()[OxO5962[7]]){alert(OxO5962[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO5962[9],OxO5962[10]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxO5962[11]]=Ox382;hiddenAction[OxO5962[11]]=OxO5962[12];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO5962[9],OxO5962[10]);if(Ox382){hiddenActionData[OxO5962[11]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Move_click(){if(isDemoMode){alert(OxO5962[6]);return false;} ;if(Event_GetSrcElement()[OxO5962[7]]){alert(OxO5962[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO5962[13],OxO5962[14]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxO5962[11]]=Ox382;hiddenAction[OxO5962[11]]=OxO5962[15];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO5962[13],OxO5962[14]);if(Ox382){hiddenActionData[OxO5962[11]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Copy_click(){if(isDemoMode){alert(OxO5962[6]);return false;} ;if(Event_GetSrcElement()[OxO5962[7]]){alert(OxO5962[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO5962[13],OxO5962[14]);function Ox221(Ox382){if(Ox382){hiddenActionData[OxO5962[11]]=Ox382;hiddenAction[OxO5962[11]]=OxO5962[16];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO5962[13],OxO5962[14]);if(Ox382){hiddenActionData[OxO5962[11]]=Ox382;return true;} else {return false;} ;return false;} ;} ;function Delete_click(){if(isDemoMode){alert(OxO5962[6]);return false;} ;if(Event_GetSrcElement()[OxO5962[7]]){alert(OxO5962[8]);return false;} ;return confirm(OxO5962[17]);} ;function EditImg_click(img){if(isDemoMode){alert(OxO5962[6]);return false;} ;var Ox387=img[OxO5962[18]][OxO5962[18]];var p=Ox387.getAttribute(OxO5962[19]);if(p!=OxO5962[10]&&p!=null){function Ox35d(Ox20a){if(Ox20a){alert(OxO5962[20]);window.PostBackAction();} ;} ;editor.SetNextDialogWindow(window);editor.ShowDialog(Ox35d,OxO5962[21]+GetDialogQueryString()+OxO5962[22]+p+OxO5962[10],window,OxO5962[23]);return true;} else {return false;} ;} ;function GetDialogQueryString(){var Ox120=OxO5962[10];if(editor.GetScriptProperty(OxO5962[24])==OxO5962[25]){Ox120=OxO5962[26];} ;return OxO5962[27]+editor.GetScriptProperty(OxO5962[28])+OxO5962[29]+editor.GetScriptProperty(OxO5962[30])+Ox120+OxO5962[31]+editor.GetScriptProperty(OxO5962[32]);} ;function NewTemplate_Click(){if(isDemoMode){alert(OxO5962[6]);return false;} ;function Ox35d(Ox20a){if(Ox20a){alert(OxO5962[33]);hiddenActionData[OxO5962[11]]=OxO5962[10];hiddenAction[OxO5962[11]]=OxO5962[34];window.PostBackAction();} ;} ;if(Browser_IsIE7()){IEprompt(Ox221,OxO5962[35],OxO5962[10]);function Ox221(Ox382){if(Ox382){Ox382=FolderDescription[OxO5962[36]]+Ox382;editor.SetNextDialogWindow(window);editor.ShowDialog(Ox35d,OxO5962[21]+GetDialogQueryString()+OxO5962[22]+Ox382+OxO5962[10],window,OxO5962[37]);return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox382=prompt(OxO5962[35],OxO5962[10]);if(Ox382){Ox382=FolderDescription[OxO5962[36]]+Ox382;editor.SetNextDialogWindow(window);editor.ShowDialog(Ox35d,OxO5962[21]+GetDialogQueryString()+OxO5962[22]+Ox382+OxO5962[10],window,OxO5962[37]);return true;} else {return false;} ;} ;} ;function GetDialogQueryString(){var Ox120=OxO5962[10];if(editor.GetScriptProperty(OxO5962[24])==OxO5962[25]){Ox120=OxO5962[26];} ;return OxO5962[27]+editor.GetScriptProperty(OxO5962[28])+OxO5962[29]+editor.GetScriptProperty(OxO5962[30])+Ox120+OxO5962[31]+editor.GetScriptProperty(OxO5962[32]);} ;function RenImg_click(img){if(isDemoMode){alert(OxO5962[6]);return false;} ;if(img[OxO5962[7]]){alert(OxO5962[8]);return false;} ;var Ox387=img[OxO5962[18]][OxO5962[18]];var Ox389;Ox389=Ox387.getAttribute(OxO5962[38])==OxO5962[39];var Ox388=Ox387.getAttribute(OxO5962[40]);var name;if(Browser_IsIE7()){var Oxca;if(Ox389){IEprompt(Ox221,OxO5962[9],Ox388);} else {var i=Ox388.lastIndexOf(OxO5962[41]);Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxO5962[41]));IEprompt(Ox221,OxO5962[35],Ox12);} ;function Ox221(Ox382){if(Ox382&&Ox382!=Ox387.getAttribute(OxO5962[40])){if(!Ox389){Ox382=Ox382+Oxca;} ;hiddenAction[OxO5962[11]]=OxO5962[42];hiddenActionData[OxO5962[11]]=(Ox389?OxO5962[43]:OxO5962[44])+OxO5962[45]+Ox387.getAttribute(OxO5962[19])+OxO5962[45]+Ox382;window.PostBackAction();} ;} ;} else {if(Ox389){name=prompt(OxO5962[9],Ox388);} else {var i=Ox388.lastIndexOf(OxO5962[41]);var Oxca=Ox388.substr(i);var Ox12=Ox388.substr(0,Ox388.lastIndexOf(OxO5962[41]));name=prompt(OxO5962[35],Ox12);if(name){name=name+Oxca;} ;} ;if(name&&name!=Ox387.getAttribute(OxO5962[40])){hiddenAction[OxO5962[11]]=OxO5962[42];hiddenActionData[OxO5962[11]]=(Ox389?OxO5962[43]:OxO5962[44])+OxO5962[45]+Ox387.getAttribute(OxO5962[19])+OxO5962[45]+name;window.PostBackAction();} ;} ;return Event_CancelEvent();} ;setMouseOver();function setMouseOver(){var FoldersAndFiles=Window_GetElement(window,OxO5962[46],true);var Ox38c=FoldersAndFiles.getElementsByTagName(OxO5962[47]);for(var i=1;i<Ox38c[OxO5962[48]];i++){var Ox387=Ox38c[i];Ox387[OxO5962[49]]= new Function(OxO5962[10],OxO5962[50]);Ox387[OxO5962[51]]= new Function(OxO5962[10],OxO5962[52]);} ;} ;function test(){alert(222);} ;function row_click(Ox387){var Ox389;Ox389=Ox387.getAttribute(OxO5962[38])==OxO5962[39];if(Ox389){if(Event_GetSrcElement()[OxO5962[53]]==OxO5962[54]){return ;} ;hiddenAction[OxO5962[11]]=OxO5962[55];hiddenActionData[OxO5962[11]]=Ox387.getAttribute(OxO5962[19]);window.PostBackAction();} else {var Ox109=Ox387.getAttribute(OxO5962[19]);hiddenFile[OxO5962[11]]=Ox109;var Ox288=Ox387.getAttribute(OxO5962[56]);Window_GetElement(window,OxO5962[57],true)[OxO5962[11]]=Ox288;var htmlcode=Ox387.getAttribute(OxO5962[58]);if(htmlcode!=OxO5962[10]&&htmlcode!=null){do_preview(htmlcode);} else {try{Actualsize();} catch(x){do_preview();} ;} ;} ;} ;function do_preview(){} ;function reset_hiddens(){if(hiddenAlert[OxO5962[11]]){alert(hiddenAlert.value);} ;if(hiddenHTML[OxO5962[11]]){do_preview(hiddenHTML.value);} ;hiddenAlert[OxO5962[11]]=OxO5962[10];hiddenHTML[OxO5962[11]]=OxO5962[10];hiddenAction[OxO5962[11]]=OxO5962[10];hiddenActionData[OxO5962[11]]=OxO5962[10];} ;Event_Attach(window,OxO5962[59],reset_hiddens);function RequireFileBrowseScript(){} ;Event_Attach(window,OxO5962[59],sortables_init);var SORT_COLUMN_INDEX;function sortables_init(){if(!document[OxO5962[60]]){return ;} ;var Ox391=document.getElementsByTagName(OxO5962[61]);for(var Ox392=0;Ox392<Ox391[OxO5962[48]];Ox392++){var Ox393=Ox391[Ox392];if(((OxO5962[63]+Ox393[OxO5962[64]]+OxO5962[63]).indexOf(OxO5962[62])!=-1)&&(Ox393[OxO5962[65]])){ts_makeSortable(Ox393);} ;} ;} ;function ts_makeSortable(Ox395){if(Ox395[OxO5962[66]]&&Ox395[OxO5962[66]][OxO5962[48]]>0){var Ox396=Ox395[OxO5962[66]][0];} ;if(!Ox396){return ;} ;for(var i=2;i<4;i++){var Ox397=Ox396[OxO5962[67]][i];var Ox219=ts_getInnerText(Ox397);Ox397[OxO5962[36]]=OxO5962[68]+Ox219+OxO5962[69];} ;} ;function ts_getInnerText(Ox29){if( typeof Ox29==OxO5962[70]){return Ox29;} ;if( typeof Ox29==OxO5962[71]){return Ox29;} ;if(Ox29[OxO5962[72]]){return Ox29[OxO5962[72]];} ;var Ox24=OxO5962[10];var Ox343=Ox29[OxO5962[73]];var Ox11=Ox343[OxO5962[48]];for(var i=0;i<Ox11;i++){switch(Ox343[i][OxO5962[75]]){case 1:Ox24+=ts_getInnerText(Ox343[i]);break ;;case 3:Ox24+=Ox343[i][OxO5962[74]];break ;;} ;} ;return Ox24;} ;function ts_resortTable(Ox39a){var Ox2a6;for(var Ox39b=0;Ox39b<Ox39a[OxO5962[73]][OxO5962[48]];Ox39b++){if(Ox39a[OxO5962[73]][Ox39b][OxO5962[53]]&&Ox39a[OxO5962[73]][Ox39b][OxO5962[53]].toLowerCase()==OxO5962[76]){Ox2a6=Ox39a[OxO5962[73]][Ox39b];} ;} ;var Ox39c=ts_getInnerText(Ox2a6);var Ox1e4=Ox39a[OxO5962[18]];var Ox39d=Ox1e4[OxO5962[77]];var Ox395=getParent(Ox1e4,OxO5962[78]);if(Ox395[OxO5962[66]][OxO5962[48]]<=1){return ;} ;var Ox39e=ts_getInnerText(Ox395[OxO5962[66]][1][OxO5962[67]][Ox39d]);var Ox39f=ts_sort_caseinsensitive;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^\d\d[\/-]\d\d[\/-]\d\d$/)){Ox39f=ts_sort_date;} ;if(Ox39e.match(/^[?]/)){Ox39f=ts_sort_currency;} ;if(Ox39e.match(/^[\d\.]+$/)){Ox39f=ts_sort_numeric;} ;SORT_COLUMN_INDEX=Ox39d;var Ox396= new Array();var Ox3a0= new Array();for(var i=0;i<Ox395[OxO5962[66]][0][OxO5962[48]];i++){Ox396[i]=Ox395[OxO5962[66]][0][i];} ;for(var Ox25=1;Ox25<Ox395[OxO5962[66]][OxO5962[48]];Ox25++){Ox3a0[Ox25-1]=Ox395[OxO5962[66]][Ox25];} ;Ox3a0.sort(Ox39f);if(Ox2a6.getAttribute(OxO5962[79])==OxO5962[80]){var Ox3a1=OxO5962[81];Ox3a0.reverse();Ox2a6.setAttribute(OxO5962[79],OxO5962[82]);} else {Ox3a1=OxO5962[83];Ox2a6.setAttribute(OxO5962[79],OxO5962[80]);} ;for(i=0;i<Ox3a0[OxO5962[48]];i++){if(!Ox3a0[i][OxO5962[64]]||(Ox3a0[i][OxO5962[64]]&&(Ox3a0[i][OxO5962[64]].indexOf(OxO5962[84])==-1))){Ox395[OxO5962[85]][0].appendChild(Ox3a0[i]);} ;} ;for(i=0;i<Ox3a0[OxO5962[48]];i++){if(Ox3a0[i][OxO5962[64]]&&(Ox3a0[i][OxO5962[64]].indexOf(OxO5962[84])!=-1)){Ox395[OxO5962[85]][0].appendChild(Ox3a0[i]);} ;} ;var Ox3a2=document.getElementsByTagName(OxO5962[76]);for(var Ox39b=0;Ox39b<Ox3a2[OxO5962[48]];Ox39b++){if(Ox3a2[Ox39b][OxO5962[64]]==OxO5962[86]){if(getParent(Ox3a2[Ox39b],OxO5962[61])==getParent(Ox39a,OxO5962[61])){Ox3a2[Ox39b][OxO5962[36]]=OxO5962[87];} ;} ;} ;Ox2a6[OxO5962[36]]=Ox3a1;} ;function getParent(Ox29,Ox3a4){if(Ox29==null){return null;} else {if(Ox29[OxO5962[75]]==1&&Ox29[OxO5962[53]].toLowerCase()==Ox3a4.toLowerCase()){return Ox29;} else {return getParent(Ox29.parentNode,Ox3a4);} ;} ;} ;function ts_sort_date(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO5962[67]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxO5962[67]][SORT_COLUMN_INDEX]);if(Ox3a6[OxO5962[48]]==10){var Ox3a8=Ox3a6.substr(6,4)+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} else {var Ox3a9=Ox3a6.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxO5962[88]+Ox3a9;} else {Ox3a9=OxO5962[89]+Ox3a9;} ;var Ox3a8=Ox3a9+Ox3a6.substr(3,2)+Ox3a6.substr(0,2);} ;if(Ox3a7[OxO5962[48]]==10){var Ox3aa=Ox3a7.substr(6,4)+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} else {Ox3a9=Ox3a7.substr(6,2);if(parseInt(Ox3a9)<50){Ox3a9=OxO5962[88]+Ox3a9;} else {Ox3a9=OxO5962[89]+Ox3a9;} ;var Ox3aa=Ox3a9+Ox3a7.substr(3,2)+Ox3a7.substr(0,2);} ;if(Ox3a8==Ox3aa){return 0;} ;if(Ox3a8<Ox3aa){return -1;} ;return 1;} ;function ts_sort_currency(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO5962[67]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxO5962[10]);var Ox3a7=ts_getInnerText(b[OxO5962[67]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxO5962[10]);return parseFloat(Ox3a6)-parseFloat(Ox3a7);} ;function ts_sort_numeric(Oxee,b){var Ox3a6=parseFloat(ts_getInnerText(Oxee[OxO5962[67]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a6)){Ox3a6=0;} ;var Ox3a7=parseFloat(ts_getInnerText(b[OxO5962[67]][SORT_COLUMN_INDEX]));if(isNaN(Ox3a7)){Ox3a7=0;} ;return Ox3a6-Ox3a7;} ;function ts_sort_caseinsensitive(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO5962[67]][SORT_COLUMN_INDEX]).toLowerCase();var Ox3a7=ts_getInnerText(b[OxO5962[67]][SORT_COLUMN_INDEX]).toLowerCase();if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} ;function ts_sort_default(Oxee,b){var Ox3a6=ts_getInnerText(Oxee[OxO5962[67]][SORT_COLUMN_INDEX]);var Ox3a7=ts_getInnerText(b[OxO5962[67]][SORT_COLUMN_INDEX]);if(Ox3a6==Ox3a7){return 0;} ;if(Ox3a6<Ox3a7){return -1;} ;return 1;} [sortables_init];RequireFileBrowseScript();var browse_Frame=Window_GetElement(window,OxO5962[90],true);var hiddenDirectory=Window_GetElement(window,OxO5962[0],true);var hiddenFile=Window_GetElement(window,OxO5962[1],true);var hiddenAlert=Window_GetElement(window,OxO5962[2],true);var hiddenAction=Window_GetElement(window,OxO5962[3],true);var hiddenActionData=Window_GetElement(window,OxO5962[4],true);var Image1=Window_GetElement(window,OxO5962[91],true);var FolderDescription=Window_GetElement(window,OxO5962[92],true);var CreateDir=Window_GetElement(window,OxO5962[93],true);var Copy=Window_GetElement(window,OxO5962[94],true);var Move=Window_GetElement(window,OxO5962[95],true);var NewTemplate=Window_GetElement(window,OxO5962[96],true);var FoldersAndFiles=Window_GetElement(window,OxO5962[46],true);var Delete=Window_GetElement(window,OxO5962[97],true);var DoRefresh=Window_GetElement(window,OxO5962[98],true);var framepreview=document.getElementById(OxO5962[100])[OxO5962[99]];var TargetUrl=Window_GetElement(window,OxO5962[57],true);var btn_zoom_in=Window_GetElement(window,OxO5962[101],true);var btn_zoom_out=Window_GetElement(window,OxO5962[102],true);var btn_Actualsize=Window_GetElement(window,OxO5962[103],true);var obj=Window_GetDialogArguments(window);var editor=obj[OxO5962[104]];var ver=getInternetExplorerVersion();if(ver>-1&&ver<=9.0){var needAdjust=true;if(ver>=8.0&&document[OxO5962[105]]){if(document[OxO5962[106]]>7){needAdjust=false;} ;} ;if(needAdjust&&(browse_Frame[OxO5962[107]]<browse_Frame[OxO5962[108]])){FoldersAndFiles[OxO5962[110]][OxO5962[109]]=OxO5962[111];} ;} ;function getInternetExplorerVersion(){var Ox3ca=-1;if(navigator[OxO5962[112]]==OxO5962[113]){var Ox3cb=navigator[OxO5962[114]];var Ox296= new RegExp(OxO5962[115]);if(Ox296.exec(Ox3cb)!=null){Ox3ca=parseFloat(RegExp.$1);} ;} ;return Ox3ca;} ;var htmlcode=OxO5962[10];function do_preview(Ox283){htmlcode=Ox283;if(Ox283==OxO5962[10]||Ox283==null){var Ox232=TargetUrl[OxO5962[11]];if(Ox232.indexOf(OxO5962[116])!=-1){document.getElementById(OxO5962[100])[OxO5962[117]]=Ox232;} else {framepreview[OxO5962[119]][OxO5962[118]][OxO5962[36]]=OxO5962[10];} ;} else {framepreview[OxO5962[119]][OxO5962[118]][OxO5962[36]]=Ox283;} ;} ;function do_insert(){var Ox232=TargetUrl[OxO5962[11]];if(Ox232.indexOf(OxO5962[116])!=-1){htmlcode=framepreview[OxO5962[119]][OxO5962[118]][OxO5962[36]];} ;htmlcode=htmlcode.replace(/[\u00A0-\u00FF|\u00FF-\uFFFF]/g,function (Oxee,b,Ox217){return OxO5962[120]+Oxee.charCodeAt(0)+OxO5962[121];} );editor.PasteHTML(htmlcode);Window_CloseDialog(window);} ;function do_Close(){Window_CloseDialog(window);} ;function Zoom_In(){if(framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]!=0){framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]*=1.1;} else {framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]=1.1;} ;} ;function Zoom_Out(){if(framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]!=0){framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]*=0.8;} else {framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]=0.8;} ;} ;function Actualsize(){framepreview[OxO5962[119]][OxO5962[118]][OxO5962[110]][OxO5962[122]]=1;do_preview(htmlcode);} ;if(Browser_IsIE7()){var _dialogPromptID=null;function IEprompt(Ox221,Ox222,Ox223){that=this;this[OxO5962[123]]=function (Ox224){val=document.getElementById(OxO5962[124])[OxO5962[11]];_dialogPromptID[OxO5962[110]][OxO5962[125]]=OxO5962[126];document.getElementById(OxO5962[124])[OxO5962[11]]=OxO5962[10];if(Ox224){val=OxO5962[10];} ;Ox221(val);return false;} ;if(Ox223==undefined){Ox223=OxO5962[10];} ;if(_dialogPromptID==null){var Ox225=document.getElementsByTagName(OxO5962[118])[0];tnode=document.createElement(OxO5962[127]);tnode[OxO5962[65]]=OxO5962[128];Ox225.appendChild(tnode);_dialogPromptID=document.getElementById(OxO5962[128]);tnode=document.createElement(OxO5962[127]);tnode[OxO5962[65]]=OxO5962[129];Ox225.appendChild(tnode);_dialogPromptID[OxO5962[110]][OxO5962[130]]=OxO5962[131];_dialogPromptID[OxO5962[110]][OxO5962[132]]=OxO5962[133];_dialogPromptID[OxO5962[110]][OxO5962[134]]=OxO5962[135];_dialogPromptID[OxO5962[110]][OxO5962[109]]=OxO5962[136];_dialogPromptID[OxO5962[110]][OxO5962[137]]=OxO5962[138];} ;var Ox226=OxO5962[139];Ox226+=OxO5962[140]+Ox222+OxO5962[141];Ox226+=OxO5962[142];Ox226+=OxO5962[143]+Ox223+OxO5962[144];Ox226+=OxO5962[145];Ox226+=OxO5962[146];Ox226+=OxO5962[147];Ox226+=OxO5962[148];Ox226+=OxO5962[149];_dialogPromptID[OxO5962[36]]=Ox226;_dialogPromptID[OxO5962[110]][OxO5962[150]]=OxO5962[151];_dialogPromptID[OxO5962[110]][OxO5962[152]]=parseInt((document[OxO5962[118]][OxO5962[153]]-315)/2)+OxO5962[154];_dialogPromptID[OxO5962[110]][OxO5962[125]]=OxO5962[155];var Ox227=document.getElementById(OxO5962[124]);try{var Ox228=Ox227.createTextRange();Ox228.collapse(false);Ox228.select();} catch(x){Ox227.focus();} ;} ;} ;if(!Browser_IsWinIE()){btn_zoom_in[OxO5962[110]][OxO5962[125]]=btn_zoom_out[OxO5962[110]][OxO5962[125]]=btn_Actualsize[OxO5962[110]][OxO5962[125]]=OxO5962[126];} ;if(CreateDir){CreateDir[OxO5962[49]]= new Function(OxO5962[156]);} ;if(Copy){Copy[OxO5962[49]]= new Function(OxO5962[156]);} ;if(Move){Move[OxO5962[49]]= new Function(OxO5962[156]);} ;if(Delete){Delete[OxO5962[49]]= new Function(OxO5962[156]);} ;if(DoRefresh){DoRefresh[OxO5962[49]]= new Function(OxO5962[156]);} ;if(btn_zoom_in){btn_zoom_in[OxO5962[49]]= new Function(OxO5962[156]);} ;if(btn_zoom_out){btn_zoom_out[OxO5962[49]]= new Function(OxO5962[156]);} ;if(btn_Actualsize){btn_Actualsize[OxO5962[49]]= new Function(OxO5962[156]);} ;if(NewTemplate){NewTemplate[OxO5962[49]]= new Function(OxO5962[156]);} ;