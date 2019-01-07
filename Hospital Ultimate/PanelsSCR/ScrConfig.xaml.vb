Imports Microsoft.Win32

Public Class ScrConfig

    Private Sub ScrConfig_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        URLvideo.Text = My.Settings.URLvideo
        URLofflineVideo.Text = My.Settings.NameFile
        If My.Settings.ModeView = 0 Then
            RPonline.IsChecked = False
            RPoffline.IsChecked = True
            GridURL.IsEnabled = False
            LoadVideo.IsEnabled = True
        ElseIf My.Settings.ModeView = 1 Then
            RPonline.IsChecked = True
            RPoffline.IsChecked = False
            GridURL.IsEnabled = True
            LoadVideo.IsEnabled = False
        End If
    End Sub

    Private Sub URLsave_Click(sender As Object, e As RoutedEventArgs) Handles URLsave.Click
        My.Settings.URLvideo = URLvideo.Text
        My.Settings.Save()
    End Sub

    Private Sub URLcancel_Click(sender As Object, e As RoutedEventArgs) Handles URLcancel.Click
        My.Settings.URLvideo = ""
        URLvideo.Text = ""
        My.Settings.Save()
    End Sub

    Private Sub LoadVideo_Click(sender As Object, e As RoutedEventArgs) Handles LoadVideo.Click
        MultimediaLocal()
    End Sub

    Public Sub MultimediaLocal()
        Dim explorador As New OpenFileDialog With {.Title = "Abrir un Archivo", .Filter = "All Files|*.*|Archivos de Imágen|*.jpg;*.png;*.gif;*.bmp|Archivos de Video|*.mp4;*.wmv|Archivos de Audio|*.mp3;*.wma"
        }
        explorador.ShowDialog()
        If explorador.SafeFileName <> "" Then
            URLofflineVideo.Text = explorador.SafeFileName
            My.Settings.NameFile = explorador.SafeFileName
            My.Settings.SourceFile = explorador.FileName
            My.Settings.Save()
        Else
            URLofflineVideo.Text = "Nombre del archivo"
            My.Settings.SourceFile = ""
            My.Settings.Save()
        End If
    End Sub

    Private Sub RPoffline_Click(sender As Object, e As RoutedEventArgs) Handles RPoffline.Click
        If RPoffline.IsChecked = True Then
            RPonline.IsChecked = False
            GridURL.IsEnabled = False
            LoadVideo.IsEnabled = True
        Else
            RPonline.IsChecked = True
            GridURL.IsEnabled = True
            LoadVideo.IsEnabled = False
        End If
    End Sub

    Private Sub RPonline_Click(sender As Object, e As RoutedEventArgs) Handles RPonline.Click
        If RPonline.IsChecked = True Then
            RPoffline.IsChecked = False
            LoadVideo.IsEnabled = False
            GridURL.IsEnabled = True
        Else
            RPoffline.IsChecked = True
            LoadVideo.IsEnabled = True
            GridURL.IsEnabled = False
        End If
    End Sub

    Private Sub RPoffline_Checked(sender As Object, e As RoutedEventArgs) Handles RPoffline.Checked
        My.Settings.ModeView = 0
        My.Settings.Save()
    End Sub

    Private Sub RPonline_Checked(sender As Object, e As RoutedEventArgs) Handles RPonline.Checked
        My.Settings.ModeView = 1
        My.Settings.Save()
    End Sub

    Private Sub ScrConfig_IsEnabledChanged(sender As Object, e As DependencyPropertyChangedEventArgs) Handles Me.IsEnabledChanged

    End Sub

    Private Sub ScrConfig_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized

    End Sub
End Class
