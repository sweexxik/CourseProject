/*
* autor: Miller Augusto S. Martins
* e-mail: miller.augusto@gmail.com
* github: miamarti
* */
(function(e,f){angular.module("ng.ckeditor",["ng"]).directive("ngCkeditor",function(){return{restrict:"E",link:function(c,d,a){d[0].innerHTML='<div id="'+a.bind+'"></div> <div class="totalTypedCharacters"></div>';var b={removeButtons:void 0!=a.removeButtons?"About,"+a.removeButtons:"About"};void 0!=a.removePlugins&&(b.removePlugins=a.removePlugins);void 0!=a.skin&&(b.skin=a.skin);CKEDITOR.appendTo(a.bind,b,c[a.bind]).on("change",function(b){c[a.bind]=b.editor.getData();void 0!=a.msnCount&&(d[0].querySelector(".totalTypedCharacters").innerHTML= a.msnCount+" "+b.editor.getData().length)})}}})})(window,document);