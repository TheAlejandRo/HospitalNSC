Imports System.Data
Imports MySql.Data.MySqlClient
Imports MaterialDesignThemes.Wpf
Imports System.ComponentModel
Imports System.Windows.Threading

Public Class Secretarias

    Dim WithEvents Dtimer As New DispatcherTimer
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim update_state As String = String.Empty

    Private Sub win_move_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_move.MouseLeftButtonDown
        DragMove()
    End Sub

    Private Sub btn_menu_Click(sender As Object, e As RoutedEventArgs) Handles btn_menu.Click
        lat_menu.SelectedIndex = -1
    End Sub

    Private Sub config_pnlespera_Selected(sender As Object, e As RoutedEventArgs) Handles config_pnlespera.Selected
        btn_menu.IsChecked = False
        DoctoresActivos.IsEnabled = False
        DoctoresActivos.Visibility = Visibility.Collapsed
        FuncionesExtras.IsEnabled = True
        FuncionesExtras.Visibility = Visibility.Visible
    End Sub

    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        btn_menu.IsChecked = False
        Dim dlgclshw As New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        btn_menu.IsChecked = False
        Dim login As New Login
        login.Show()
        Me.Close()
    End Sub

    Private Sub pnl_espera_Selected(sender As Object, e As RoutedEventArgs) Handles pnl_espera.Selected
        btn_menu.IsChecked = False
        Dim panel As New MainWindow
        panel.Show()
    End Sub

    Private Sub Secretarias_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dtimer.Interval = New TimeSpan(0, 0, 5)
        Dtimer.Start()
    End Sub

    Private Sub Activo()
        Try
            update_state = "UPDATE usuarios SET estado='1' WHERE idusuario='" & IDscr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Secretarias_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='0' WHERE idusuario='" & IDscr.Text & "'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub generador_ticket_Selected(sender As Object, e As RoutedEventArgs) Handles generador_ticket.Selected
        btn_menu.IsChecked = False
        DoctoresActivos.IsEnabled = True
        DoctoresActivos.Visibility = Visibility.Visible
        FuncionesExtras.IsEnabled = False
        FuncionesExtras.Visibility = Visibility.Collapsed
    End Sub

    Private Sub Dtimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ActiveDR()
        DRnames()
    End Sub

    Private Sub ActiveDR()
        Try
            conexion.Open()
            consulta = "SELECT idusuario, estado FROM usuarios WHERE tipo_usuario='Doctor'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            adaptador.Fill(tabla)
            If tabla.Rows(0)(0).ToString = "6" And tabla.Rows(0)(1).ToString = "1" Then
                DR1.IsEnabled = True
            ElseIf tabla.Rows(0)(1).ToString = "0" Then
                DR1.IsEnabled = False
            End If
            If tabla.Rows(1)(0).ToString = "7" And tabla.Rows(1)(1).ToString = "1" Then
                DR2.IsEnabled = True
            ElseIf tabla.Rows(1)(1).ToString = "0" Then
                DR2.IsEnabled = False
            End If
            If tabla.Rows(2)(0).ToString = "8" And tabla.Rows(2)(1).ToString = "1" Then
                DR3.IsEnabled = True
            ElseIf tabla.Rows(2)(1).ToString = "0" Then
                DR3.IsEnabled = False
            End If
            If tabla.Rows(3)(0).ToString = "9" And tabla.Rows(3)(1).ToString = "1" Then
                DR4.IsEnabled = True
            ElseIf tabla.Rows(3)(1).ToString = "0" Then
                DR4.IsEnabled = False
            End If
            If tabla.Rows(4)(0).ToString = "10" And tabla.Rows(4)(1).ToString = "1" Then
                DR5.IsEnabled = True
            ElseIf tabla.Rows(4)(1).ToString = "0" Then
                DR5.IsEnabled = False
            End If
            If tabla.Rows(5)(0).ToString = "11" And tabla.Rows(5)(1).ToString = "1" Then
                DR6.IsEnabled = True
            ElseIf tabla.Rows(5)(1).ToString = "0" Then
                DR6.IsEnabled = False
            End If
            If tabla.Rows(6)(0).ToString = "12" And tabla.Rows(6)(1).ToString = "1" Then
                DR7.IsEnabled = True
            ElseIf tabla.Rows(6)(1).ToString = "0" Then
                DR7.IsEnabled = False
            End If
            If tabla.Rows(7)(0).ToString = "13" And tabla.Rows(7)(1).ToString = "1" Then
                DR8.IsEnabled = True
            ElseIf tabla.Rows(7)(1).ToString = "0" Then
                DR8.IsEnabled = False
            End If
            If tabla.Rows(8)(0).ToString = "14" And tabla.Rows(8)(1).ToString = "1" Then
                DR9.IsEnabled = True
            ElseIf tabla.Rows(8)(1).ToString = "0" Then
                DR9.IsEnabled = False
            End If
            If tabla.Rows(9)(0).ToString = "15" And tabla.Rows(9)(1).ToString = "1" Then
                DR10.IsEnabled = True
            ElseIf tabla.Rows(9)(1).ToString = "0" Then
                DR10.IsEnabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub DRnames()
        Try
            conexion.Open()
            consulta = "SELECT idusuario, docnom, apnom FROM usuarios WHERE tipo_usuario='Doctor'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            adaptador.Fill(tabla)
            If tabla.Rows(0)(0).ToString = "6" Then
                Drname1.Text = tabla.Rows(0)(1).ToString
                Drfristname1.Text = tabla.Rows(0)(2).ToString
            End If
            If tabla.Rows(1)(0).ToString = "7" Then
                Drname2.Text = tabla.Rows(1)(1).ToString
                Drfristname2.Text = tabla.Rows(1)(2).ToString
            End If
            If tabla.Rows(2)(0).ToString = "8" Then
                Drname3.Text = tabla.Rows(2)(1).ToString
                Drfristname3.Text = tabla.Rows(2)(2).ToString
            End If
            If tabla.Rows(3)(0).ToString = "9" Then
                Drname4.Text = tabla.Rows(3)(1).ToString
                Drfristname4.Text = tabla.Rows(3)(2).ToString
            End If
            If tabla.Rows(4)(0).ToString = "10" Then
                Drname5.Text = tabla.Rows(4)(1).ToString
                Drfristname5.Text = tabla.Rows(4)(2).ToString
            End If
            If tabla.Rows(5)(0).ToString = "11" Then
                Drname6.Text = tabla.Rows(5)(1).ToString
                Drfristname6.Text = tabla.Rows(5)(2).ToString
            End If
            If tabla.Rows(6)(0).ToString = "12" Then
                Drname7.Text = tabla.Rows(6)(1).ToString
                Drfristname7.Text = tabla.Rows(6)(2).ToString
            End If
            If tabla.Rows(7)(0).ToString = "13" Then
                Drname8.Text = tabla.Rows(7)(1).ToString
                Drfristname8.Text = tabla.Rows(7)(2).ToString
            End If
            If tabla.Rows(8)(0).ToString = "14" Then
                Drname9.Text = tabla.Rows(8)(1).ToString
                Drfristname9.Text = tabla.Rows(8)(2).ToString
            End If
            If tabla.Rows(9)(0).ToString = "15" Then
                Drname10.Text = tabla.Rows(9)(1).ToString
                Drfristname10.Text = tabla.Rows(9)(2).ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub
End Class