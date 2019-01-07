Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.ComponentModel
Imports System.Windows.Threading

Class MainWindow

    Dim Dtimer As DispatcherTimer = New DispatcherTimer
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub win_mov_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_mov.MouseLeftButtonDown
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        End If
        DragMove()
    End Sub

    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        Dtimer.Stop()
        OfflineView.Close()
        Me.Finalize()
        Me.Close()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ModoVisual()
        Listado_tikets.IsReadOnly = True
        AddHandler Dtimer.Tick, AddressOf Dtimer_Tick
        Dtimer.Interval = New TimeSpan(0, 0, 1)
        Dtimer.Start()
    End Sub

    Private Sub Dtimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            conexion.Open()
            consulta = "SELECT Doctor, Tiket, Clinica FROM pacientes WHERE estado_paciente='1'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            Listado_tikets.ItemsSource = tabla.DefaultView
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub ModoVisual()
        If My.Settings.ModeView = 1 Then
            web_view.IsEnabled = True
            web_view.Visibility = Visibility.Visible
            If My.Settings.URLvideo <> "" Then
                web_view.Source = New Uri(My.Settings.URLvideo)
            Else
                web_view.Source = New Uri("https://www.youtube.com")
            End If
        ElseIf My.Settings.ModeView = 0 Then
            OfflineView.IsEnabled = True
            OfflineView.Visibility = Visibility.Visible
            If My.Settings.SourceFile <> "" Then
                OfflineView.Source = New Uri(My.Settings.SourceFile)
            Else
                OfflineView.IsEnabled = False
                OfflineView.Visibility = Visibility.Collapsed
                My.Settings.ModeView = 1
                My.Settings.Save()
            End If
        End If
    End Sub

    Private Sub Listado_tikets_BeginningEdit(sender As Object, e As DataGridBeginningEditEventArgs) Handles Listado_tikets.BeginningEdit
        Listado_tikets.CancelEdit()
    End Sub

    Private Sub OfflineView_MediaEnded(sender As Object, e As RoutedEventArgs) Handles OfflineView.MediaEnded
        OfflineView.Position = New TimeSpan(0, 0, 0)
        OfflineView.Play()
    End Sub

    Private Sub OfflineView_MediaOpened(sender As Object, e As RoutedEventArgs) Handles OfflineView.MediaOpened
        If OfflineView.HasVideo = False Then
            Portada.IsEnabled = True
            Portada.Visibility = Visibility.Visible
        ElseIf OfflineView.HasVideo = False And OfflineView.HasAudio = False Then
            Portada.IsEnabled = False
            Portada.Visibility = Visibility.Collapsed
        End If
    End Sub
End Class