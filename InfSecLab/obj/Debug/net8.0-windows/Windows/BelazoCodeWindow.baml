   M S B A M L   `   `   `  ����D  @InfSecLab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:InfSecLab	InfSecLab  5   	        InfSecLab.BelazoCodeWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:InfSecLab     5   	   -   5          � Title$  BelazoCodeWindow��5   	   $	��450��6"   $	��800��6/    � 
ResizeMode$ NoResize=�5	   	   .��5
      � ���	#FFC6FFC3 � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�6,   $��10q�6G   $��0��6S   .+�5      �� -   5         ��Name$ SelectLanguageTextBlock��5       �� TextAlignment$ Center=�69   $��Выберите язык��6P   $��10q�6e   $��20(�6q   �� 5      -   5         ��Name$ LanguageComboBox��5      $��10q�61    �� SelectedIndex$ 0��6=   .@�5      �� -   5        $ RussianLanguage��5      $��Русский язык��65   $��20(�6I   �� 5      -   5        $ EnglishLanguage��5      $#��Английский язык��65   $��20(�6L   5      �� 5      -   5        $ InputKeyTextBlock��5      $ Center=�63   $��Введите ключ��6J   $��10q�6^   $��20(�6j   �� 5      -   5         ��Name$ InputKeyTextBox��5      	 �� TextWrapping$
	 Wrap=�6/   $��20(�6C   $��10q�6Q   $��30��6�   6�   �� 5      -   5        
 ��Name$
 OpenFileButton��5      $��Открыть файл��6-   $��30��6D   $��20(�6P   ���	#FF80FF83$��10q�6u   6�   �� 5      -	   5        $
 WriteFileButton��5      $"��Записать в файл��6.   $��30��6H   $��20(�6T   ���	#FF80FF83$��10q�6y   6�   5      �� 5   
   -
   5         ��Name$ MainAlgStackPanel��5      $ Vertical=�60   $��Center=�6G   $��5,0,5,0q�6b   $��1��6s   .+�5      �� -   5        $ OriginalMessageTextBlock��5      $ Center=�6:   $)��#Исходное сообщение��6Q   $��5q�6k   $��20(�6v   �� 5      -   5        $ OriginalMessageTextBox��5      $
	 Wrap=�66   $	��200��6J   $	��110��6V   $��5q�6c   �� 5      -   5        $% EncryptDecryptButtonsStackPanel��5      $ 
Horizontal=�6B   $��Center=�6[   .+�5      �� -   5        $
 EncryptButton��5      $��Закодировать��60   $��30��6G   $��20(�6S   ���	#FF80FF83$��10q�6�   �� 5       -   5        $
 DecryptButton��5       $��Декодировать��60   $��30��6G   $��20(�6S   ���	#FF80FF83$��10q�6�   5!      �� 5"      -   5        $ EncryptMessageTextBlock��5"      $ Center=�69   $5��/Закодированное сообщение��6P   $��5q�6p   $��20(�6{   �� 5#      -   5        $ EncryptMessageTextBox��5#      $
	 Wrap=�65   $	��200��6I   $	��110��6U   $��5q�6b   �� 5$      -   5        $
 ClearButton��5$      $��Очистить��6*   $��Center=�6=   $��30��6Z   $��20(�6f   
�������$��5q�6�   5%      �� 5&   
   $��2��6   $ Vertical=�6%   $��Center=�6<   $��10q�6W   .+�5'      �� -   5        $" EntropyInputMessageTextBlock��5'      $ Center=�6>   $<��6Энтропия исходного
сообщения��6U   $��20(�6}   �� 5(      -   5        $  EntropyInputMessageTextBox��5(      $
	 Wrap=�6:   $��20(�6N   $��10q�6\   $��25��6h   $��60��6t    �� 
IsReadOnly$
 True��6   �� 5)      -   5        $&  EntropyEncryptedMessageTextBlock��5)      $ Center=�6B   $H��BЭнтропия закодированного
сообщения��6Y   $��20(�6�   �� 5*      -   5        $$ EntropyEncryptedMessageTextBox��5*      $
	 Wrap=�6>   $��20(�6R   $��10q�6`   $��25��6l   $��60��6x   $
 True��6�   5+      5,      5-      