   M S B A M L   `   `   `  ����E  ARestApiDoc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:RestApiDoc.ViewModelsRestApiDoc.ViewModels  5      %    RestApiDoc.Pages.AdminUsersPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :
viewmodels#clr-namespace:RestApiDoc.ViewModels     5        � 	Resources  5
       �� 
TargetType   MaterialDesignRaisedButton �� BasedOn �� Property �� Value��%�   '��       7   �� 5   
   " ��6   8   .�5      ��  � ��$ 0 0 10 0q�6'   �� 5       � ��$ 0q�60   �� 5       � �� �#00b4d86+   5      5      .��5      � 
������  � RowDefinitions 5   
   �� 5      6   �� 5      $
��Auto �6   6,   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	UsersList��5      $��10q�5   #   
�������   CanUserAddRows$ False��5   #      CanUserDeleteRows$ False��5   #   	   AutoGenerateColumns$	 False��5   #   ��5   #   �� *Users+
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Код��6%      
IsReadOnly$
 True��6h   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *Id+ �� UpdateSourceTrigger$ 	LostFocus=�6h     5      $ Email��6%    64   �� *Email+$ 	LostFocus=�  5       $ Пароль��6%    65   �� *
Password+$ 	LostFocus=�4  .System.Windows.Controls.DataGridCheckBoxColumn  5!      $ 
Админ��6)    68   �� *	IsAdmin+$ 	LostFocus=�4  .System.Windows.Controls.DataGridTemplateColumn  5"         CellTemplate 5#      �� 5$      .R�5%      �� $��Удалить��6%   ��5&   %   ��  �� Path$# DataContext.DeleteUserCommand� �� RelativeSource ��  �� AncestorType"  ��5'   %   �� *SelectedItem+ �� ElementName$ 	UsersList��5(      5*        5+       5,      �� 5-      .R�5.      �� $��Обновить��6%   ��5/   %   �� $# DataContext.UpdateUserCommand� �� "  ��50   %   �� *SelectedItem+$ 	UsersList��51      53      55      �� 56   
   $��Добавить��6   $��1��6$   $��
10 0 10 10q�61   ��6E   �� *CreateUserCommand+57      58      