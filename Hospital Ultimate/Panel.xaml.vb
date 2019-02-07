Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Threading
Imports System.Windows.Forms
Imports System.Windows.Media.Animation
Imports MaterialDesignThemes.Wpf

Public Enum Texts
    BoxOne
    BoxTwo
End Enum

Public Class Panel
    Private dubAnim As New DoubleAnimation()
    Private dubAnim2 As New DoubleAnimation()
    Private NewsTimer As New DispatcherTimer
    Dim leadText As Texts = Texts.BoxOne
    Dim Dtimer As DispatcherTimer = New DispatcherTimer
    Dim WithEvents TiempCall1 As New DispatcherTimer
    Dim WithEvents tiempcall As New DispatcherTimer
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim conexion1 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta1 As String = String.Empty
    Dim comando1 As MySqlCommand
    Dim adaptador1 As MySqlDataAdapter
    Dim tabla1 As New DataTable
    Dim conexion2 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta2 As String = String.Empty
    Dim comando2 As MySqlCommand
    Dim timecall As Integer = 0

    Private Sub win_mov_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_mov.MouseLeftButtonDown
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        End If
        DragMove()
    End Sub

    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        Dtimer.Stop()
        tiempcall.Stop()
        TiempCall1.Stop()
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
        TiempCall1.Interval = New TimeSpan(0, 0, 1)
        TiempCall1.Start()

        BoxOne.Content = My.Settings.Banner
        BoxTwo.Content = My.Settings.Banner

        dubAnim.From = Me.ActualWidth
        dubAnim.To = -Me.ActualWidth
        dubAnim.SpeedRatio = 0.05
        AddHandler dubAnim.Completed, AddressOf dubAnim_Completed
        Timeline.SetDesiredFrameRate(dubAnim, 320)
        BoxOne.BeginAnimation(Canvas.LeftProperty, dubAnim)

        dubAnim2.From = Me.ActualWidth
        dubAnim2.To = -Me.ActualWidth
        dubAnim2.SpeedRatio = 0.05
        Timeline.SetDesiredFrameRate(dubAnim2, 320)
        AddHandler dubAnim2.Completed, AddressOf dubAnim2_Completed

        AddHandler NewsTimer.Tick, AddressOf NewsTimer_Tick
        NewsTimer.Interval = New TimeSpan(0, 0, 0.9)
        NewsTimer.Start()
    End Sub

    Private Sub NewsTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Dim BoxOneLocation As Point = BoxOne.TranslatePoint(New Point(0, 0), ViewingBox)
        Dim BoxTwoLocation As Point = BoxTwo.TranslatePoint(New Point(0, 0), ViewingBox)

        If leadText = Texts.BoxOne Then
            Dim loc As Double = BoxOneLocation.X + Me.ActualWidth
            If loc < ViewingBox.ActualWidth / 1.5 Then
                BoxTwo.BeginAnimation(Canvas.LeftProperty, dubAnim2)
                NewsTimer.Stop()
            End If
        Else
            Dim loc As Double = BoxTwoLocation.X + Me.ActualWidth
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
            consulta = "SELECT Doctor, Tiket FROM pacientes WHERE estado_paciente='1'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            Listado_tikets.ItemsSource = tabla.DefaultView
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
        HoraM.Content = TimeOfDay.ToShortTimeString
        FechaM.Content = Date.Today.ToShortDateString
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

    Private Sub tiempcall_Tick(sender As Object, e As EventArgs) Handles tiempcall.Tick
        Dim clientCall As New MessageDialogPaciente
        timecall += 1
        TiempCall1.Stop()
        clientCall.Header.Content = tabla1.Rows(0)(0).ToString
        clientCall.CodPac.Text = tabla1.Rows(0)(1).ToString
        If timecall = 2 Then
            DialogHost.Show(clientCall, "RootClient")
        ElseIf timecall = 5 Then
            Try
                conexion2.Open()
                consulta2 = "UPDATE pacientes SET CallSpeak='0' WHERE tiket='" & clientCall.CodPac.Text & "' AND Doctor='" & clientCall.Header.Content & "'"
                comando2 = New MySqlCommand(consulta2, conexion2)
                comando2.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
                Log.e("Error con excepción y traza", ex, New StackFrame(True))
            Finally
                conexion2.Close()
            End Try
            Dialog.IsOpen = False
            timecall = 0
            TiempCall1.Start()
            tiempcall.Stop()
        End If
    End Sub

    Private Sub TiempCall1_Tick(sender As Object, e As EventArgs) Handles TiempCall1.Tick
        Try
            conexion1.Open()
            consulta1 = "SELECT Doctor, Tiket FROM pacientes WHERE estado_paciente='1' AND CallSpeak='1'"
            comando1 = New MySqlCommand(consulta1, conexion1)
            adaptador1 = New MySqlDataAdapter(comando1)
            tabla1.Clear()
            adaptador1.Fill(tabla1)
            If tabla1.Rows.Count <> 0 Then
                tiempcall.Interval = New TimeSpan(0, 0, 1)
                tiempcall.Start()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion1.Close()
        End Try
    End Sub
End Class
