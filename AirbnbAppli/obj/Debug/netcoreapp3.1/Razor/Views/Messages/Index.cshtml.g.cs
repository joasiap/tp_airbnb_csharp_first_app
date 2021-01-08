#pragma checksum "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ef4f9a7948d53932b2ba1e3295ed7e0b2f1c817"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Messages_Index), @"mvc.1.0.view", @"/Views/Messages/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\_ViewImports.cshtml"
using AirbnbAppli;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\_ViewImports.cshtml"
using AirbnbAppli.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ef4f9a7948d53932b2ba1e3295ed7e0b2f1c817", @"/Views/Messages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fa948fe80eb6b63c5dd69ddfbe0dbaf8a20aa8a", @"/Views/_ViewImports.cshtml")]
    public class Views_Messages_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml"
  
    var idProprietaire = ViewData["idProprietaire"];
    var idUtilisateurAuthentifie = Convert.ToInt32(HttpContextAccessor.HttpContext.Session.GetInt32("userId"));
    var token = antiforgery.GetAndStoreTokens(HttpContextAccessor.HttpContext).RequestToken;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 12 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml"
Write(Html.Hidden("utilisateur", idUtilisateurAuthentifie));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 13 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml"
Write(Html.Hidden("idProprietaire", idProprietaire));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container-fluid"">
    <div class=""chat_window"">
        <div class=""top_menu"">
            <div class=""title"">Messagerie</div>
        </div>
        <ul class=""messages""></ul>
        <div class=""bottom_wrapper clearfix"">
            <div class=""message_input_wrapper"">
                <input class=""message_input"" placeholder=""Votre message..."" />
            </div>
            <div class=""send_message"">
                <div class=""icon""></div>
                <div class=""text"">Envoyer</div>
            </div>
        </div>
    </div>
    <div class=""message_template"">
        <li class=""message"">
            <div class=""avatar""></div>
            <div class=""text_wrapper"">
                <div class=""text""></div>
            </div>
        </li>
    </div>

    <div class=""row"">
        <div id=""js-errors"" class=""col-lg-12""></div>
    </div>
</div>
<script src=""//code.jquery.com/jquery-1.11.3.min.js""></script>
<script>
    (function () {
        let utilisateurA");
            WriteLiteral(@"uthentifie = $('#utilisateur').val();
        let idProprietaire = $('#idProprietaire').val();
        let $jsErrors = $('#js-errors');
        var Message;
        Message = function (arg) {
            this.text = arg.text, this.message_side = arg.message_side;
            this.draw = function (_this) {
                return function () {
                    var $message;
                    $message = $($('.message_template').clone().html());
                    $message.addClass(_this.message_side).find('.text').html(_this.text);
                    $('.messages').append($message);
                    return setTimeout(function () {
                        return $message.addClass('appeared');
                    }, 0);
                };
            }(this);
            return this;
        };
        $(function () {
            var getMessageText, message_side, sendMessage;
            message_side = 'right';
            // récupère le message dans input
            getMessageTex");
            WriteLiteral(@"t = function () {
                var $message_input;
                $message_input = $('.message_input');
                return $message_input.val();
            };

            // envoi du message dans la messagerie -> effet visuel uniquement
            sendMessage = function (text, message_side) {
                var $messages, message;
                if (text.trim() === '') {
                    return;
                }
                $('.message_input').val('');
                $messages = $('.messages');
                message = new Message({
                    text: text,
                    message_side: message_side
                });
                message.draw();
                message_side = '';
                return $messages.animate({ scrollTop: $messages.prop('scrollHeight') }, 300);
            };

            // permet d'enregistrer le message dans la BDD 
            posterMessage = function () {

                  // var token = document.querySelector('");
            WriteLiteral(@"input[name=""__RequestVerificationToken""]').getAttribute(""value"");
                /*
                 *  headers: {
                        ""RequestVerificationToken"": token
                    },*/
                let textMessage = getMessageText();
                let data = { idProprietaire: idProprietaire, idUtilisateur: utilisateurAuthentifie, contenu: textMessage };
                // AJAX -> poster un message
                $.ajax({
                    type: ""POST"",
                    url: '");
#nullable restore
#line 105 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml"
                     Write(Url.Action("Create", "Messages"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    data: JSON.stringify(data),
                    dataType: 'json',
                    contentType: ""application/json; charset=utf-8"",
                    processData: false,
                    error: function (response) {
                        viderErreurs($jsErrors);
                        afficherErreurs(""Echec lors de l'envoi de message"", $jsErrors);
                    },
                    success: function (response) {
                        if (response.success) {
                            message_side = 'right';
                            return sendMessage(textMessage, message_side);
                        } else {
                            viderErreurs($jsErrors);
                            afficherErreurs(""Une erreur s'est produite lors de l'envoi de message"", $jsErrors);
                        }
                },

                });
            // fin appel AJAX

            }

            // envoi de messages suite au clic
          ");
            WriteLiteral(@"  $('.send_message').click(function (e) {
                posterMessage();
            });
            // envoi de message si on presse Enter
            $('.message_input').keyup(function (e) {
                if (e.which === 13) {
                    posterMessage();
                }
            });




            // AJAX -> récupérer les messages
            $.ajax({
                type: ""GET"",
                url: '");
#nullable restore
#line 146 "C:\Users\pietr\OneDrive\Bureau\ENI CDA\coursC#ASP\AirbnbAppli\AirbnbAppli\Views\Messages\Index.cshtml"
                 Write(Url.Action("GetMessages", "Messages"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                data: ""idProprietaire="" + idProprietaire + ""&idUtilisateur="" + utilisateurAuthentifie,
                processData: false,
                success: function (response, status, xhr) {
                    // la réponse est un STRING avec des données JSON donc il faut la parser pour obtenir un objet JSON en JS
                    let messagesjsonArray = JSON.parse(response);
                    $.each(messagesjsonArray, function (i, val) {
                        // messages utilisateur d'un côté
                        if (utilisateurAuthentifie == val.Emetteur.Id) {
                            message_side = 'right';
                        }
                        else {
                            message_side = 'left';
                        }
                        sendMessage(val.Contenu, message_side);
                    });
                },
            });
            // fin appel AJAX récupération de messages
        });



        // fonction qui affiche");
            WriteLiteral(@" les erreurs js/jquery dans le div #js-errors
        function afficherErreurs(message, jsErrors) {
            //on vide le div des erreurs précédentes s'il y en avait pour afficher de nouvelles
            jsErrors.empty();
            // si on laisse la classe tout le temps, il y aura un encadré rouge vide sur la page
            if (jsErrors.hasClass(""alert alert-danger mx-auto"")) {
                jsErrors.removeClass(""alert alert-danger mx-auto"");
                jsErrors.fadeOut(300); // effet de disparition
            }
            jsErrors.addClass(""alert alert-danger mx-auto"");
            jsErrors.append(message);
            jsErrors.fadeIn(300); // effet d'apparition
            return setTimeout(function () {
                return viderErreurs(jsErrors);
            }, 2000);
        }
        // fonction qui vide le div #js-errors de toutes les erreurs qu'il contient
        function viderErreurs(jsErrors) {
            jsErrors.empty();
            if (jsErrors.hasClass(""a");
            WriteLiteral("lert alert-danger mx-auto\")) {\r\n                jsErrors.removeClass(\"alert alert-danger mx-auto\");\r\n               // jsErrors.fadeOut(500);\r\n            }\r\n        }\r\n    }.call(this));\r\n</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Antiforgery.IAntiforgery antiforgery { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
