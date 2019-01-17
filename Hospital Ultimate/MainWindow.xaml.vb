Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Threading
Imports System.Windows.Forms
Imports System.Windows.Media.Animation

Public Enum Texts
    BoxOne
    BoxTwo
End Enum

Class MainWindow

    Private dubAnim As New DoubleAnimation()
    Private dubAnim2 As New DoubleAnimation()
    Private NewsTimer As New Windows.Threading.DispatcherTimer()
    Dim leadText As Texts = Texts.BoxOne
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
        web_view.Dispose()
        Me.Finalize()
        Me.Close()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ModoVisual()
        Listado_tikets.IsReadOnly = True
        AddHandler Dtimer.Tick, AddressOf Dtimer_Tick
        Dtimer.Interval = New TimeSpan(0, 0, 1)
        Dtimer.Start()

        dubAnim.From = ViewingBox.ActualWidth
        dubAnim.To = -BoxOne.ActualWidth
        dubAnim.SpeedRatio = 0.05
        AddHandler dubAnim.Completed, AddressOf dubAnim_Completed
        Timeline.SetDesiredFrameRate(dubAnim, 320)
        BoxOne.BeginAnimation(Canvas.LeftProperty, dubAnim)

        dubAnim2.From = ViewingBox.ActualWidth
        dubAnim2.To = -BoxTwo.ActualWidth
        dubAnim2.SpeedRatio = 0.05
        Timeline.SetDesiredFrameRate(dubAnim2, 320)
        AddHandler dubAnim2.Completed, AddressOf dubAnim2_Completed

        AddHandler NewsTimer.Tick, AddressOf NewsTimer_Tick
        NewsTimer.Interval = New TimeSpan(0, 0, 0.9)
        NewsTimer.Start()

        'If My.Settings.Banner <> "" Then
        '    Banner.Text = My.Settings.Banner
        '    If Banner.Width < Me.Width Then
        '        Banner.VerticalAlignment = VerticalAlignment.Center
        '    ElseIf Banner.Width > Me.Width Then
        '        Banner.VerticalAlignment = VerticalAlignment.Center
        '        Banner.Margin = New Thickness(0, 0, -Banner.Width, 0)
        '        'ds.Interval = New TimeSpan(0, 0, 1)
        '        'ds.Start()
        '    End If
        'ElseIf My.Settings.Banner = "" Then
        '    Banner.Text = "Hospital Nuestra Señora del Carmen"
        '    Banner.VerticalAlignment = VerticalAlignment.Top
        'End If
    End Sub

    Private Sub NewsTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Dim BoxOneLocation As Point = BoxOne.TranslatePoint(New Point(0, 0), ViewingBox)
        Dim BoxTwoLocation As Point = BoxTwo.TranslatePoint(New Point(0, 0), ViewingBox)

        If leadText = Texts.BoxOne Then
            Dim loc As Double = BoxOneLocation.X + BoxOne.ActualWidth
            If loc < ViewingBox.ActualWidth / 1.5 Then
                BoxTwo.BeginAnimation(Canvas.LeftProperty, dubAnim2)
                NewsTimer.Stop()
            End If
        Else
            Dim loc As Double = BoxTwoLocation.X + BoxTwo.ActualWidth
            If loc < ViewingBox.ActualWidth / 1.5 Then
                BoxOne.BeginAnimation(Canvas.LeftProperty, dubAnim)
                NewsTimer.Stop()
            End If
        End If
    End Sub

    Private Sub dubAnim_Completed(ByVal sender As Object, ByVal e As EventArgs)
        leadText = Texts.BoxTwo
        NewsTimer.Start()
    End Sub

    Private Sub dubAnim2_Completed(ByVal sender As Object, ByVal e As EventArgs)
        leadText = Texts.BoxOne
        NewsTimer.Start()
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
        HoraM.Content = TimeOfDay.ToShortTimeString
        FechaM.Content = DateTime.Today.ToShortDateString
    End Sub

    Private Sub ModoVisual()
        If My.Settings.ModeView = 1 Then
            web_view.IsEnabled = True
            web_view.Visibility = Visibility.Visible
            If My.Settings.URLvideo <> "" Then
                web_view.Address = My.Settings.URLvideo
            Else
                web_view.Address = "https://www.youtube.com"
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

    'Private Sub ds_Tick(sender As Object, e As EventArgs) Handles Ds.Tick
    '    '    Dim correr As Double
    '    '    correr += 1
    '    '    If Banner.Margin.Left <= Width Then
    '    '        Banner.Margin = New Thickness(0, 0, correr, 0)
    '    '    End If
    'End Sub
End Class