   M S B A M L   `   `   `  ����E  ARestApiDoc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:RestApiDoc.ViewModelsRestApiDoc.ViewModels  5   	   "    RestApiDoc.Views.TestsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :
viewmodels#clr-namespace:RestApiDoc.ViewModels     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  TestsWindow��5	   	   $	��600��6   $
��1000��6*   ���#2b373d � WindowStartupLocation$ CenterScreen=�6L    � 	Resources 5
       �� 
TargetType   MaterialDesignRaisedButton �� BasedOn �� Property �� Value # MaterialDesignAccentRadioButton   MaterialDesignAccentCheckBox  MaterialDesignTextBox.  (MaterialDesignThemes.Wpf.TextFieldAssist��%�  '��       7   '�� v     '	� �     7  '�� :    7  '�� �    7  �� 5   
   " ��6   8   .�5      ��  � ��$ 0q�60   �� 5       � �� �#00b4d86+   5      �� 5   
   " ��6   .�5      ��  � �� �#fff6+   5      �� 5   
   " 	�6   8   .�5      ��  � �� �#fff6+   �� 5       � ��$ 5 0 5 0q�6'   5      �� 5   
   " ��6   8   .�5      ��  � �� �#fff6+   �� 5       � ��$ 5 0 5 0q�6'   5      �� 5   
   " ��6   8   .�5      ��  � �� �#fff6,   �� 5       � �� �#fff6+   �� 5       � ��$ 5 0 5 0q�6'   �� 5       �@ UnderlineBrush �#fff6B   5      5       .��5!      � .+�5"   
   � -   5         �Name$ 
PanelRules��5"       � RowDefinitions 5#      �� 5$      6"   �� 5%      $
��Auto �6    60   .+�5'      � .+�5(      �� -   5        	 ��Name$	 RulesTestRtf��5(      $��0��64   
 �� 
IsReadOnly$

 True��6A   $��0q�6S   ���#fff$��10q�6y   5)      �� 5*      -   5        $��1��5*   0   $��10q�6=   .��6I   Начать тест6V   5+      ]� 5,   
   -   5         ]�Name$ DockPanelTest��5,      $y�	Collapsed=�6+   .+�5-      �� $��Bottom=�6   .@�5.      �� $
��Left=�6    .��5/      �� -   5         ��Name$ lblTimeTest��5/      ���#00061   50      �� 51      $��Right=�6    $��Right=�67   .��52      ��  �� Orientation$ 
Horizontal=�6!   .+�53      �� -   5        .��53   9   Завершить6D   �� 54      -   5        $��5q�54   !   $	��5 0q�6-   ��6:   0  *MaterialDesignThemes.Wpf.PackIconExtension  *Minus+6X   �� 55      -   5        $��5q�55   !   ��6-     *Plus+6J   56      57      58      �� 59      $��0��6   $��Stretch=�6+   $��Disabled=�6I   $
��Auto=�6r   .��5:      �� -	   5         ��Name$ QuestionsPanel��5:      $
��10 5q�65   6E   5;      5<      5=      5>      