   M S B A M L   `   `   `  ���� Q  MElysium, Version=2.0.1042.0, Culture=neutral, PublicKeyToken=afa220db249e5b15D/clr-namespace:Elysium.Controls;assembly=ElysiumElysium.Controls   StarSpot   StarSpot.SkillsMoreWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /tb http://www.hardcodet.net/taskbar     DControls/clr-namespace:Elysium.Controls;assembly=Elysium     /metro#http://schemas.codeplex.com/elysium   7params*http://schemas.codeplex.com/elysium/params   -     � WindowStartupLocation$  CenterScreen=�   Elysium.Parameters.Design   Theme$
 DarkX�   ContrastBrush$ White��   Elysium.AccentBrushes   Blue    Elysium.Parameters.Manager   AccentBrush# Z    Theme$
 DarkX�$��426.059��$��433.309��$��Disabled=� � 
ResizeMode$ CanMinimize=�$��2q� � Topmost .  � Title$ StarSpot��	 J� IsHitTestVisible	 . 
 � ShowInTaskbar
 .   � WindowStyle$ 
ToolWindow=�.��� ���	#FF171717.+��� $��
Conditions��$
��Left=�$��	10,10,0,0q�$	��Top=�$
��Bold"�$��15(����	#FF0097FF�� -    ��Name$ use_infight_cbx��$��Use infight��$
��Left=�$��
10,360,0,0q�$	��Top=����	#FF818181�� -   $ skill_charge��$��Charge��$
��Left=�$��140,360,0,0q�$	��Top=����	#FF818181� $
��Left=�$��31��$��	10,43,0,0q�$	��Top=�$	��197�����	#FF1D1D1D.+��� -    ��Name$ health_under_tbx��$
��Left=�$��26��$��	130,3,0,0q� �� TextWrapping$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If health is under X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��31��$��	10,79,0,0q�$	��Top=�$	��197�����	#FF1D1D1D.+��� -   $ shield_under_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If shield is under X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24���� $��Target��$
��Left=�$��
10,115,0,0q�$	��Top=����	#FFC7C7C7$��27��� $
��Left=�$��31��$��
10,142,0,0q�$	��Top=�$	��197�����	#FF111111.+��� -   $ target_health_under_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If health is under X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��$��2��� $
��Left=�$��31��$��
10,178,0,0q�$	��Top=�$	��197�����	#FF111111.+��� -   $ target_shield_under_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If shield is under X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��31��$��
212,43,0,0q�$	��Top=�$	��197�����	#FF1D1D1D.+��� -   $ health_over_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If health is over X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��31��$��
212,79,0,0q�$	��Top=�$	��197�����	#FF1D1D1D.+��� -	   $ shield_over_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If shield is over X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��31��$��212,142,0,0q�$	��Top=�$	��197�����	#FF111111.+��� -
   $ target_health_over_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If health is over X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��$��2��� $
��Left=�$��31��$��212,178,0,0q�$	��Top=�$	��197�����	#FF111111.+��� -   $ target_shield_over_tbx��$
��Left=�$��26��$��	130,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��If shield is over X %��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��26��$��
10,323,0,0q�$	��Top=�$	��396�����	#FFB00000.+��� $$��To disable a function, type: 0��$
��Left=�$	��Top=����	#FF4D0000�� -   $ skill_interrupt_cbx��$��	Interrupt��$
��Left=�$��247,360,0,0q�$	��Top=����	#FF818181� $
��Left=�$��31��$��
10,287,0,0q�$	��Top=�$	��122�����	#FF111111.+��� -   $ repeat_skill_tbx��$
��Left=�$��26��$��55,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��64�����	#FF292929�� $��Repeat��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��31��$��
10,214,0,0q�$	��Top=�$	��399�����	#FF111111.+��� -   $ special_power_tbx��$
��Left=�$��26��$��	307,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��89�����	#FF292929�� -    ��Name$ special_power_lbl��$�� ��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��� $
��Left=�$��31��$��
10,250,0,0q�$	��Top=�$	��399�����	#FF111111.+��� -   $ special_power_under_tbx��$
��Left=�$��26��$��	307,3,0,0q�$
 Wrap=�$��0��$	��Top=�$��89�����	#FF292929�� -   $ special_power_under_lbl��$�� ��$
��Left=�$��3,3,0,0q�$	��Top=����	#FF818181$��24��