var OxO336d=["inp_src","btnbrowse","AlternateText","inp_id","longDesc","Align","optNotSet","optLeft","optRight","optTexttop","optAbsMiddle","optBaseline","optAbsBottom","optBottom","optMiddle","optTop","Border","bordercolor","bordercolor_Preview","inp_width","imgLock","inp_height","constrain_prop","HSpace","VSpace","outer","img_demo","onclick","IMG","src","width","height","value","cssText","style","","src_cetemp","id","vspace","hspace","border","borderColor"," ","backgroundColor","align","alt","[[ValidNumber]]","[[ValidID]]","checked","Load.ashx?type=image\x26file=locked.gif","Load.ashx?type=image\x26file=1x1.gif","length"];var inp_src=Window_GetElement(window,OxO336d[0],true);var btnbrowse=Window_GetElement(window,OxO336d[1],true);var AlternateText=Window_GetElement(window,OxO336d[2],true);var inp_id=Window_GetElement(window,OxO336d[3],true);var longDesc=Window_GetElement(window,OxO336d[4],true);var Align=Window_GetElement(window,OxO336d[5],true);var optNotSet=Window_GetElement(window,OxO336d[6],true);var optLeft=Window_GetElement(window,OxO336d[7],true);var optRight=Window_GetElement(window,OxO336d[8],true);var optTexttop=Window_GetElement(window,OxO336d[9],true);var optAbsMiddle=Window_GetElement(window,OxO336d[10],true);var optBaseline=Window_GetElement(window,OxO336d[11],true);var optAbsBottom=Window_GetElement(window,OxO336d[12],true);var optBottom=Window_GetElement(window,OxO336d[13],true);var optMiddle=Window_GetElement(window,OxO336d[14],true);var optTop=Window_GetElement(window,OxO336d[15],true);var Border=Window_GetElement(window,OxO336d[16],true);var bordercolor=Window_GetElement(window,OxO336d[17],true);var bordercolor_Preview=Window_GetElement(window,OxO336d[18],true);var inp_width=Window_GetElement(window,OxO336d[19],true);var imgLock=Window_GetElement(window,OxO336d[20],true);var inp_height=Window_GetElement(window,OxO336d[21],true);var constrain_prop=Window_GetElement(window,OxO336d[22],true);var HSpace=Window_GetElement(window,OxO336d[23],true);var VSpace=Window_GetElement(window,OxO336d[24],true);var outer=Window_GetElement(window,OxO336d[25],true);var img_demo=Window_GetElement(window,OxO336d[26],true);btnbrowse[OxO336d[27]]=function btnbrowse_onclick(){function Ox35d(Ox13e){if(Ox13e){function Actualsize(){var Ox7d=document.createElement(OxO336d[28]);Ox7d[OxO336d[29]]=Ox13e;if(Ox7d[OxO336d[30]]>0&&Ox7d[OxO336d[31]]>0){inp_width[OxO336d[32]]=Ox7d[OxO336d[30]];inp_height[OxO336d[32]]=Ox7d[OxO336d[31]];FireUIChanged();} else {setTimeout(Actualsize,400);} ;} ;inp_src[OxO336d[32]]=Ox13e;FireUIChanged();setTimeout(Actualsize,400);} ;} ;editor.SetNextDialogWindow(window);if(Browser_IsSafari()){editor.ShowSelectImageDialog(Ox35d,inp_src.value,inp_src);} else {editor.ShowSelectImageDialog(Ox35d,inp_src.value);} ;} ;UpdateState=function UpdateState_Image(){img_demo[OxO336d[34]][OxO336d[33]]=element[OxO336d[34]][OxO336d[33]];if(Browser_IsWinIE()){img_demo.mergeAttributes(element);} ;if(element[OxO336d[29]]){img_demo[OxO336d[29]]=element[OxO336d[29]];} else {img_demo.removeAttribute(OxO336d[29]);} ;} ;SyncToView=function SyncToView_Image(){var src;src=element.getAttribute(OxO336d[29])+OxO336d[35];if(element.getAttribute(OxO336d[36])){src=element.getAttribute(OxO336d[36])+OxO336d[35];} ;inp_src[OxO336d[32]]=src;inp_width[OxO336d[32]]=element[OxO336d[30]];inp_height[OxO336d[32]]=element[OxO336d[31]];inp_id[OxO336d[32]]=element[OxO336d[37]];if(element[OxO336d[38]]<=0){VSpace[OxO336d[32]]=OxO336d[35];} else {VSpace[OxO336d[32]]=element[OxO336d[38]];} ;if(element[OxO336d[39]]<=0){HSpace[OxO336d[32]]=OxO336d[35];} else {HSpace[OxO336d[32]]=element[OxO336d[39]];} ;Border[OxO336d[32]]=element[OxO336d[40]];if(Browser_IsWinIE()){bordercolor[OxO336d[32]]=element[OxO336d[34]][OxO336d[41]];} else {var arr=revertColor(element[OxO336d[34]].borderColor).split(OxO336d[42]);bordercolor[OxO336d[32]]=arr[0];} ;bordercolor[OxO336d[34]][OxO336d[43]]=bordercolor[OxO336d[32]]||OxO336d[35];bordercolor[OxO336d[34]][OxO336d[43]]=bordercolor[OxO336d[32]];bordercolor_Preview[OxO336d[34]][OxO336d[43]]=bordercolor[OxO336d[32]];Align[OxO336d[32]]=element[OxO336d[44]];AlternateText[OxO336d[32]]=element[OxO336d[45]];longDesc[OxO336d[32]]=element.getAttribute(OxO336d[4]);} ;SyncTo=function SyncTo_Image(element){element[OxO336d[29]]=inp_src[OxO336d[32]];element.setAttribute(OxO336d[36],inp_src.value);element[OxO336d[40]]=Border[OxO336d[32]];element[OxO336d[39]]=HSpace[OxO336d[32]];element[OxO336d[38]]=VSpace[OxO336d[32]];try{element[OxO336d[30]]=inp_width[OxO336d[32]];element[OxO336d[31]]=inp_height[OxO336d[32]];} catch(er){alert(OxO336d[46]);return false;} ;if(element[OxO336d[34]][OxO336d[30]]||element[OxO336d[34]][OxO336d[31]]){try{element[OxO336d[34]][OxO336d[30]]=inp_width[OxO336d[32]];element[OxO336d[34]][OxO336d[31]]=inp_height[OxO336d[32]];} catch(er){alert(OxO336d[46]);return false;} ;} ;var Ox376=/[^a-z\d]/i;if(Ox376.test(inp_id.value)){alert(OxO336d[47]);return ;} ;var Ox4f1=longDesc[OxO336d[32]];element[OxO336d[37]]=inp_id[OxO336d[32]];element[OxO336d[44]]=Align[OxO336d[32]];element[OxO336d[45]]=AlternateText[OxO336d[32]];if(Ox4f1){element.setAttribute(OxO336d[4],Ox4f1);} else {element.removeAttribute(OxO336d[4]);} ;element[OxO336d[34]][OxO336d[41]]=bordercolor[OxO336d[32]];if(element[OxO336d[30]]==0){element.removeAttribute(OxO336d[30]);} ;if(element[OxO336d[31]]==0){element.removeAttribute(OxO336d[31]);} ;if(element[OxO336d[39]]==OxO336d[35]){element.removeAttribute(OxO336d[39]);} ;if(element[OxO336d[38]]==OxO336d[35]){element.removeAttribute(OxO336d[38]);} ;if(element[OxO336d[37]]==OxO336d[35]){element.removeAttribute(OxO336d[37]);} ;if(element[OxO336d[44]]==OxO336d[35]){element.removeAttribute(OxO336d[44]);} ;if(element[OxO336d[40]]==OxO336d[35]){element.removeAttribute(OxO336d[40]);} ;} ;function toggleConstrains(){if(constrain_prop[OxO336d[48]]){imgLock[OxO336d[29]]=OxO336d[49];checkConstrains(OxO336d[30]);} else {imgLock[OxO336d[29]]=OxO336d[50];} ;} ;var checkingConstrains=false;function checkConstrains(Ox7a){if(checkingConstrains){return ;} ;checkingConstrains=true;try{var Ox8,Ox2d;if(constrain_prop[OxO336d[48]]){var Ox7d=document.createElement(OxO336d[28]);Ox7d[OxO336d[29]]=inp_src[OxO336d[32]];var Ox429=Ox7d[OxO336d[30]];var Ox42a=Ox7d[OxO336d[31]];if((Ox429>0)&&(Ox42a>0)){var Ox74=inp_width[OxO336d[32]];var Ox73=inp_height[OxO336d[32]];if(Ox7a==OxO336d[30]){if(Ox74[OxO336d[51]]==0||isNaN(Ox74)){inp_width[OxO336d[32]]=OxO336d[35];inp_height[OxO336d[32]]=OxO336d[35];} else {Ox73=parseInt(Ox74*Ox42a/Ox429);inp_height[OxO336d[32]]=Ox73;} ;} ;if(Ox7a==OxO336d[31]){if(Ox73[OxO336d[51]]==0||isNaN(Ox73)){inp_width[OxO336d[32]]=OxO336d[35];inp_height[OxO336d[32]]=OxO336d[35];} else {Ox74=parseInt(Ox73*Ox429/Ox42a);inp_width[OxO336d[32]]=Ox74;} ;} ;} ;} ;} finally{checkingConstrains=false;} ;} ;bordercolor[OxO336d[27]]=bordercolor_Preview[OxO336d[27]]=function bordercolor_onclick(){SelectColor(bordercolor,bordercolor_Preview);} ;