   M S B A M L   `   `   `  ����E  ARestApiDoc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:RestApiDoc.ViewModelsRestApiDoc.ViewModels  5   	   !    RestApiDoc.Views.AuthWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   Bd2http://schemas.microsoft.com/expression/blend/2008      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :
viewmodels#clr-namespace:RestApiDoc.ViewModels     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
AuthWindow��5
   	   $	��400��6   $	��250��6(   ���#2b373d � 
ResizeMode$ NoResize=�6J    � WindowStartupLocation$ CenterScreen=�6`    � 	Resources 5       �� 
TargetType   MaterialDesignTextBox �� BasedOn �� Property �� Value.  (MaterialDesignThemes.Wpf.TextFieldAssist  MaterialDesignRaisedButton��%�  '��       7   '�� �     '�� "    7  �� 5   
   " ��6   8   .�5      ��  � ��$ 0 0 0 20q�6'   �� 5       � �� �#fff6+   �� 5       � �� �#fff6,   �� 5       �@ UnderlineBrush �#fff6B   5      �� 5   
   " ��6   .�5      ��  � �� �#fff6+   5      �� 5   
   " ��6   8   .�5      ��  � ��$ 0q�60   �� 5       � �� �#00b4d86+   �� 5       � �� �#fff6+   5      5      .��5      � $��
20 0 20 20q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5!   
   �� $��0��6   .+�5"      �� $��Email��6   �� 5#      ��6   �� *Email+�� 5%      $��Пароль��6   �� 5&      ��6   �� *
Password+5'      �� 5(   
   -   5        $��
Войти��5(      $��1��67   5)      5*      