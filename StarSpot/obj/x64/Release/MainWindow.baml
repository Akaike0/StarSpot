   M S B A M L   `   `   `  ���� Q  MElysium, Version=2.0.1042.0, Culture=neutral, PublicKeyToken=afa220db249e5b15D/clr-namespace:Elysium.Controls;assembly=ElysiumElysium.Controls   StarSpot   StarSpot.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /tb http://www.hardcodet.net/taskbar     DControls/clr-namespace:Elysium.Controls;assembly=Elysium     /metro#http://schemas.codeplex.com/elysium   7params*http://schemas.codeplex.com/elysium/params   -     � WindowStartupLocation$  CenterScreen=�   Elysium.Parameters.Design   Theme$
 DarkX�   ContrastBrush$ White��   Elysium.AccentBrushes   Blue    Elysium.Parameters.Manager   AccentBrush# Z    Theme$
 DarkX�$	��365��$	��452��$��Disabled=� � 
ResizeMode$ CanMinimize=�$��2q� � Topmost .  J� IsHitTestVisible .    Elysium.Controls.Window	   IsMainWindow	 .  
 � Icon$
 Images/client_Icon.png��.��� ���	#FF171717.+��� -    ��Name$ charactername_tbx��$��	10,36,0,0q�$	��Top=�$
��Left=�$	��341���� -    ��Name$ test_lbl��$��Character Name��$
��Left=�$��	10,10,0,0q�$	��Top=����	#FF00AEFF�� ���	#FF303030$
��Left=�$��1��$��	10,76,0,0q�$	��Top=�$	��341���� $��HP��$
��Left=�$��	10,82,0,0q�$	��Top=����	#FFFF0046$��2�� J� RenderTransformOrigin$ 	0.6,0.423&�� -    �Name$ character_health_pgbx��$
��Left=�$��32��$��
10,108,0,0q�$	��Top=�$	��341�����	#FF212121���	#FF0082EE���	#FF3E3E3E"   Elysium.Controls.ProgressBar  -     Name$ character_health_pgb��$
��Left=�$��8��$��130,120,0,0q�$	��Top=�$	��101��$ 0.5,0.5&����	#FF0281D6���	#FF252525z�V� .~���  �� ScaleX$
 3.36Q� �� ScaleY$	 3.8Q��� �� T� �� $��Shield Power��$
��Left=�$��
10,150,0,0q�$	��Top=����	#FF00A2FF� -   $ character_shield_pgbx��$
��Left=�$��32��$��
10,176,0,0q�$	��Top=�$	��341�����	#FF212121���	#FF0082EE���	#FF3E3E3E  -   $ character_shield_pgb��$
��Left=�$��8��$��130,188,0,0q�$	��Top=�$	��101��$ 0.5,0.5&����	#FF0281D6���	#FF252525z�V� .~��� $
 3.36Q�$	 3.8Q��� �� T� �� -    ��Name$ 	start_btn��$��Start (END)��$��270,370,0,0q�$	��Top=����	#FF659DFF$
��Left=�$��81���� -	   $ 	spots_btn��$��Spots��$
��Left=�$��
13,370,0,0q�$	��Top=�$��75�����	#FF659DFF�� -
   $ Options_btn��$��Config��$
��Left=�$��
93,370,0,0q�$	��Top=�$��75�����	#FF659DFF�� -   $��Radar��$
��Left=�$��173,370,0,0q�$	��Top=�$��75�����	#FF659DFF�� $	��Log��$
��Left=�$��
10,226,0,0q�$	��Top=����	#FFFF6800�� -    ��Name$ log_tbx��" �� VerticalScrollBarVisibility$
 Auto=�$
��Left=�$	��102��$��
10,252,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��341�����	#FF2E2E2E$	��Top=�$��10(��� -   $ client_version_tbx��$��	10,36,0,0q�$	��Top=�$
��Left=�$	��341���� -   ���	#FF659DFF$��1q�$
��Left=�$��13��$��
336,18,0,0q�$	��Top=�$��15�����	#FF017BCD