var OxO2396=["inp_type","inp_name","inp_value","row_txt1","inp_Size","row_txt2","inp_MaxLength","row_img","inp_src","btnbrowse","row_img2","sel_Align","optNotSet","optLeft","optRight","optTexttop","optAbsMiddle","optBaseline","optAbsBottom","optBottom","optMiddle","optTop","inp_Border","row_img3","inp_width","inp_height","row_img4","inp_HSpace","inp_VSpace","row_img5","AlternateText","inp_id","row_txt3","inp_access","row_txt4","inp_index","row_chk","inp_checked","row_txt5","inp_Disabled","row_txt6","inp_Readonly","onclick","value","Name","name","id","src","type","checked","disabled","readOnly","tabIndex","","accessKey","size","maxLength","width","height","vspace","hspace","border","align","alt","text","display","style","none","password","hidden","radio","checkbox","submit","reset","button","image","className","class"];var inp_type=Window_GetElement(window,OxO2396[0],true);var inp_name=Window_GetElement(window,OxO2396[1],true);var inp_value=Window_GetElement(window,OxO2396[2],true);var row_txt1=Window_GetElement(window,OxO2396[3],true);var inp_Size=Window_GetElement(window,OxO2396[4],true);var row_txt2=Window_GetElement(window,OxO2396[5],true);var inp_MaxLength=Window_GetElement(window,OxO2396[6],true);var row_img=Window_GetElement(window,OxO2396[7],true);var inp_src=Window_GetElement(window,OxO2396[8],true);var btnbrowse=Window_GetElement(window,OxO2396[9],true);var row_img2=Window_GetElement(window,OxO2396[10],true);var sel_Align=Window_GetElement(window,OxO2396[11],true);var optNotSet=Window_GetElement(window,OxO2396[12],true);var optLeft=Window_GetElement(window,OxO2396[13],true);var optRight=Window_GetElement(window,OxO2396[14],true);var optTexttop=Window_GetElement(window,OxO2396[15],true);var optAbsMiddle=Window_GetElement(window,OxO2396[16],true);var optBaseline=Window_GetElement(window,OxO2396[17],true);var optAbsBottom=Window_GetElement(window,OxO2396[18],true);var optBottom=Window_GetElement(window,OxO2396[19],true);var optMiddle=Window_GetElement(window,OxO2396[20],true);var optTop=Window_GetElement(window,OxO2396[21],true);var inp_Border=Window_GetElement(window,OxO2396[22],true);var row_img3=Window_GetElement(window,OxO2396[23],true);var inp_width=Window_GetElement(window,OxO2396[24],true);var inp_height=Window_GetElement(window,OxO2396[25],true);var row_img4=Window_GetElement(window,OxO2396[26],true);var inp_HSpace=Window_GetElement(window,OxO2396[27],true);var inp_VSpace=Window_GetElement(window,OxO2396[28],true);var row_img5=Window_GetElement(window,OxO2396[29],true);var AlternateText=Window_GetElement(window,OxO2396[30],true);var inp_id=Window_GetElement(window,OxO2396[31],true);var row_txt3=Window_GetElement(window,OxO2396[32],true);var inp_access=Window_GetElement(window,OxO2396[33],true);var row_txt4=Window_GetElement(window,OxO2396[34],true);var inp_index=Window_GetElement(window,OxO2396[35],true);var row_chk=Window_GetElement(window,OxO2396[36],true);var inp_checked=Window_GetElement(window,OxO2396[37],true);var row_txt5=Window_GetElement(window,OxO2396[38],true);var inp_Disabled=Window_GetElement(window,OxO2396[39],true);var row_txt6=Window_GetElement(window,OxO2396[40],true);var inp_Readonly=Window_GetElement(window,OxO2396[41],true);btnbrowse[OxO2396[42]]=function btnbrowse_onclick(){function Ox35d(Ox13e){if(Ox13e){inp_src[OxO2396[43]]=Ox13e;FireUIChanged();SyncTo(element);} ;} ;editor.SetNextDialogWindow(window);if(Browser_IsSafari()){editor.ShowSelectImageDialog(Ox35d,inp_src.value,inp_src);} else {editor.ShowSelectImageDialog(Ox35d,inp_src.value);} ;} ;UpdateState=function UpdateState_Input(){} ;SyncToView=function SyncToView_Input(){if(element[OxO2396[44]]){inp_name[OxO2396[43]]=element[OxO2396[44]];} ;if(element[OxO2396[45]]){inp_name[OxO2396[43]]=element[OxO2396[45]];} ;inp_id[OxO2396[43]]=element[OxO2396[46]];inp_value[OxO2396[43]]=(element[OxO2396[43]]).trim();inp_src[OxO2396[43]]=element[OxO2396[47]];inp_type[OxO2396[43]]=element[OxO2396[48]];inp_checked[OxO2396[49]]=element[OxO2396[49]];inp_Disabled[OxO2396[49]]=element[OxO2396[50]];inp_Readonly[OxO2396[49]]=element[OxO2396[51]];if(element[OxO2396[52]]==0){inp_index[OxO2396[43]]=OxO2396[53];} else {inp_index[OxO2396[43]]=element[OxO2396[52]];} ;if(element[OxO2396[54]]){inp_access[OxO2396[43]]=element[OxO2396[54]];} ;if(element[OxO2396[55]]){if(element[OxO2396[55]]==20){inp_Size[OxO2396[43]]=OxO2396[53];} else {inp_Size[OxO2396[43]]=element[OxO2396[55]];} ;} ;if(element[OxO2396[56]]){if(element[OxO2396[56]]==2147483647||element[OxO2396[56]]<=0){inp_MaxLength[OxO2396[43]]=OxO2396[53];} else {inp_MaxLength[OxO2396[43]]=element[OxO2396[56]];} ;} ;if(element[OxO2396[57]]){inp_width[OxO2396[43]]=element[OxO2396[57]];} ;if(element[OxO2396[58]]){inp_height[OxO2396[43]]=element[OxO2396[58]];} ;if(element[OxO2396[59]]){inp_HSpace[OxO2396[43]]=element[OxO2396[59]];} ;if(element[OxO2396[60]]){inp_VSpace[OxO2396[43]]=element[OxO2396[60]];} ;if(element[OxO2396[61]]){inp_Border[OxO2396[43]]=element[OxO2396[61]];} ;if(element[OxO2396[62]]){sel_Align[OxO2396[43]]=element[OxO2396[62]];} ;if(element[OxO2396[63]]){alt[OxO2396[43]]=element[OxO2396[63]];} ;switch((element[OxO2396[48]]).toLowerCase()){case OxO2396[64]:;case OxO2396[68]:row_img[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img3[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img4[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img5[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_chk[OxO2396[66]][OxO2396[65]]=OxO2396[67];break ;;case OxO2396[69]:row_img[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img3[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img4[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img5[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_chk[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt1[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt3[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt4[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt5[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt6[OxO2396[66]][OxO2396[65]]=OxO2396[67];break ;;case OxO2396[70]:;case OxO2396[71]:row_img[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img3[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img4[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img5[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt1[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt6[OxO2396[66]][OxO2396[65]]=OxO2396[67];break ;;case OxO2396[72]:;case OxO2396[73]:;case OxO2396[74]:row_chk[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img3[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img4[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_img5[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt1[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt6[OxO2396[66]][OxO2396[65]]=OxO2396[67];break ;;case OxO2396[75]:row_chk[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt1[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt2[OxO2396[66]][OxO2396[65]]=OxO2396[67];row_txt6[OxO2396[66]][OxO2396[65]]=OxO2396[67];break ;;} ;} ;SyncTo=function SyncTo_Input(element){element[OxO2396[45]]=inp_name[OxO2396[43]];if(element[OxO2396[44]]){element[OxO2396[44]]=inp_name[OxO2396[43]];} else {if(element[OxO2396[45]]){element.removeAttribute(OxO2396[45],0);element[OxO2396[44]]=inp_name[OxO2396[43]];} else {element[OxO2396[44]]=inp_name[OxO2396[43]];} ;} ;element[OxO2396[46]]=inp_id[OxO2396[43]];if(inp_src[OxO2396[43]]){element[OxO2396[47]]=inp_src[OxO2396[43]];} ;element[OxO2396[49]]=inp_checked[OxO2396[49]];element[OxO2396[43]]=inp_value[OxO2396[43]];element.setAttribute(OxO2396[43],inp_value.value);element[OxO2396[50]]=inp_Disabled[OxO2396[49]];element[OxO2396[51]]=inp_Readonly[OxO2396[49]];element[OxO2396[54]]=inp_access[OxO2396[43]];element[OxO2396[52]]=inp_index[OxO2396[43]];element[OxO2396[56]]=inp_MaxLength[OxO2396[43]];element[OxO2396[57]]=inp_width[OxO2396[43]];element[OxO2396[58]]=inp_height[OxO2396[43]];element[OxO2396[59]]=inp_HSpace[OxO2396[43]];element[OxO2396[60]]=inp_VSpace[OxO2396[43]];element[OxO2396[61]]=inp_Border[OxO2396[43]];element[OxO2396[62]]=sel_Align[OxO2396[43]];element[OxO2396[63]]=AlternateText[OxO2396[43]];try{element[OxO2396[55]]=inp_Size[OxO2396[43]];} catch(e){element[OxO2396[55]]=20;} ;if(element[OxO2396[52]]==OxO2396[53]){element.removeAttribute(OxO2396[52]);} ;if(element[OxO2396[54]]==OxO2396[53]){element.removeAttribute(OxO2396[54]);} ;if(element[OxO2396[56]]==OxO2396[53]){element.removeAttribute(OxO2396[56]);} ;if(element[OxO2396[55]]==0){element.removeAttribute(OxO2396[55]);} ;if(element[OxO2396[57]]==0){element.removeAttribute(OxO2396[57]);} ;if(element[OxO2396[58]]==0){element.removeAttribute(OxO2396[58]);} ;if(element[OxO2396[60]]==OxO2396[53]){element.removeAttribute(OxO2396[60]);} ;if(element[OxO2396[59]]==OxO2396[53]){element.removeAttribute(OxO2396[59]);} ;if(element[OxO2396[46]]==OxO2396[53]){element.removeAttribute(OxO2396[46]);} ;if(element[OxO2396[44]]==OxO2396[53]){element.removeAttribute(OxO2396[44]);} ;if(element[OxO2396[63]]==OxO2396[53]){element.removeAttribute(OxO2396[63]);} ;if(element[OxO2396[62]]==OxO2396[53]){element.removeAttribute(OxO2396[62]);} ;if(element[OxO2396[76]]==OxO2396[53]){element.removeAttribute(OxO2396[77]);} ;if(element[OxO2396[76]]==OxO2396[53]){element.removeAttribute(OxO2396[76]);} ;switch((element[OxO2396[48]]).toLowerCase()){case OxO2396[64]:;case OxO2396[68]:;case OxO2396[69]:;case OxO2396[70]:;case OxO2396[71]:;case OxO2396[72]:;case OxO2396[73]:;case OxO2396[74]:element.removeAttribute(OxO2396[58]);element.removeAttribute(OxO2396[61]);element.removeAttribute(OxO2396[47]);break ;;case OxO2396[75]:break ;;} ;} ;