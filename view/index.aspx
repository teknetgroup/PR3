<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PR3.view.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title />
        
        <link href="../library/css/style.css" rel="stylesheet" />

        <script src="/library/js/jquery-1.8.2.js"></script>

        <link href="../library/css/style.css" rel="stylesheet" />
        <link href="/library/css/jquery.Jcrop.css" rel="stylesheet" />
    
        <script src="/library/js/jquery.Jcrop.js"></script>
        <script src="../library/js/bootstrap-filestyle.js"></script>
        <script src="../library/js/bootstrap-filestyle.min.js"></script>
        <script src="../library/js/bootstrap.js"></script>

        <link href="../library/css/bootstrap-theme.css" rel="stylesheet" />
        <link href="../library/css/bootstrap-theme.min.css" rel="stylesheet" />
        <link href="../library/css/bootstrap.css" rel="stylesheet" />
        <link href="../library/css/bootstrap.min.css" rel="stylesheet" />
        <link href="../library/css/jquery.Jcrop.min.css" rel="stylesheet" />
    
        <script src="../library/js/bootstrap.min.js"></script>
        <script src="../library/js/jquery-1.7.1.min.js"></script>
    
        <link href="../library/css/style.css" rel="stylesheet" />
    
        <script src="/library/js/jquery-1.8.2.js"></script>
    
        <link href="../library/css/style.css" rel="stylesheet" />
        <link href="/library/css/jquery.Jcrop.css" rel="stylesheet" />
    
        <script src="/library/js/jquery.Jcrop.js"></script>
    
    
        <meta name="viewport" content="width=device-width"/>

    
        <link rel="stylesheet" media="handheld, only screen and (max-device-width: 1600px)" type="text/css" href="style.css" />
      
    </head>
    
    <body class="body1">
        <center>
            <form id="form" runat="server" class="embed-responsive-item">

                
          
                 <asp:Panel ID="Panel2" class="pan2" runat="server" Height="142px">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/img/cle.gif"  Width="105px" />
                 </asp:Panel>

                 <asp:Label ID="lMOub" runat="server" Text="Mot de passe oublié?" Visible="false" CssClass="text-Oubli" />
                <div class = "panel-body">
                  <asp:Panel ID="Panel1" runat="server" class="pan1" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Width="588px" Height="254px" style="margin-top: 0px">
                    <table class="auto-style23" style="margin-top: 30px;">
                        <tr>
                            <td class="auto-style25" style="line-height: 4;">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Login" />
                                </div>
                            </td>
                            
                            <td class="auto-style26">
                                <asp:TextBox class="form-control" ID="login" runat="server" Height="45px" Width="320px" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="auto-style25">
                                <div class="form-group">
                                    <asp:Label ID="Mdp" runat="server" Text="Mot de passe" />
                                    
                                    <asp:Label ID="lMail" runat="server" Text="Votre addresse e-mail" Visible="false" />
                                </div>
                            </td>
                            
                            <td class="auto-style26">
                                <asp:TextBox ID="passe" runat="server" class="form-control" Height="45px" type="password" Width="318px" style="margin: 15px;" />
                                
                                <asp:TextBox ID="mail" runat="server" class="form-control" Height="45px" type="email" Width="320px" style="margin:15px;" Visible="false" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="auto-style25" />
                            
                            <td class="auto-style26">
                                <asp:Button ID="Button2" runat="server" class=" btn btn-primary" OnClick="Button2_Click" Text="Se connecter" Width="150px" />
                                <asp:Button ID="Button1" runat="server" class=" btn btn-primary" OnClick="Button1_Click" Text="Annuler" Width="150px" />
                                <asp:Button ID="Button3" runat="server" class=" btn btn-primary" OnClick="Button3_Click" Text="Envoyer le mot de passe" Width="200px" Visible="false" />
                                <asp:Button ID="Button4" type="reset" runat="server" class=" btn btn-primary" OnClick="Button4_Click" Text="Retour" Width="100px" Visible="false" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="auto-style25" />
                            
                            <td class="auto-style26">
                                <asp:LinkButton ID="LinkForgotten" runat="server" OnClick="LinkForgotten_Click" Text="Mot de passe oublié" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
              </div>
            </form>
        </center>
    </body>
</html>
