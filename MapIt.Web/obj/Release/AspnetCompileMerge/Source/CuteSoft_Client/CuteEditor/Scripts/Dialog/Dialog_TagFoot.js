var OxO275c=["btn_editinwin","btnok","btncc","controlparent","display","style","none","onclick","nocancel","length","nodeName","SELECT","INPUT","TEXTAREA","isnotinput","1","skipAutoFireChanged","onpropertychange","","OnPropertyChange()","onchange","if(!syncingtoview)FireUIChanged()","onkeypress","onkeyup"];var btn_editinwin=Window_GetElement(window,OxO275c[0],true);var btnok=Window_GetElement(window,OxO275c[1],true);var btncc=Window_GetElement(window,OxO275c[2],true);var controlparent=Window_GetElement(window,OxO275c[3],true);btn_editinwin[OxO275c[5]][OxO275c[4]]=OxO275c[6];btn_editinwin[OxO275c[7]]=function btn_editinwin_onclick(){} ;if(Window_GetDialogTop(window)[OxO275c[8]]){btncc[OxO275c[5]][OxO275c[4]]=OxO275c[6];} ;btnok[OxO275c[7]]=function btnok_onclick(){Window_SetDialogReturnValue(window,true);Window_CloseDialog(window);} ;btncc[OxO275c[7]]=function btncc_onclick(){Window_SetDialogReturnValue(window,false);Window_CloseDialog(window);} ;function HookChangeEvents(){var Ox31=Element_GetAllElements(controlparent);for(var i=0;i<Ox31[OxO275c[9]];i++){var Ox43=Ox31[i];if(Ox43[OxO275c[10]]==OxO275c[11]||Ox43[OxO275c[10]]==OxO275c[12]||Ox43[OxO275c[10]]==OxO275c[13]){if(Ox43.getAttribute(OxO275c[14])==OxO275c[15]||Ox43.getAttribute(OxO275c[16])==OxO275c[15]){continue ;} ;Event_Attach(Ox43,OxO275c[17], new Function(OxO275c[18],OxO275c[19]));Event_Attach(Ox43,OxO275c[20], new Function(OxO275c[18],OxO275c[21]));Event_Attach(Ox43,OxO275c[22], new Function(OxO275c[18],OxO275c[21]));Event_Attach(Ox43,OxO275c[23], new Function(OxO275c[18],OxO275c[21]));Event_Attach(Ox43,OxO275c[7], new Function(OxO275c[18],OxO275c[21]));} ;} ;} ;HookChangeEvents();SyncToViewInternal();setInterval(UpdateState,300);