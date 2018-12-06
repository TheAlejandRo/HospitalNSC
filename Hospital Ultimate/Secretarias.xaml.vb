Imports System.Data
Imports MySql.Data.MySqlClient
Imports MaterialDesignThemes.Wpf
Imports System.ComponentModel

Public Class Secretarias

    Dim conexion As New MySqlConnection("host=127.0.0.1; user=root; database=dbturnos")
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

    Private Sub generador_ticket_Selected(sender As Object, e As RoutedEventArgs) Handles generador_ticket.Selected
        btn_menu.IsChecked = False
    End Sub

    Private Sub config_pnlespera_Selected(sender As Object, e As RoutedEventArgs) Handles config_pnlespera.Selected
        btn_menu.IsChecked = False
    End Sub

    Private Sub btn_close_Selected(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        btn_menu.IsChecked = False
        Dim dlgclshw As New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        btn_menu.IsChecked = False
        Dim panel As New MainWindow
        Dim login As New Login
        login.Show()
        panel.Close()
        Me.Close()
    End Sub

    Private Sub pnl_espera_Selected(sender As Object, e As RoutedEventArgs) Handles pnl_espera.Selected
        Dim panel As New MainWindow
        panel.Show()
    End Sub

    Private Sub Secretarias_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Try
            conexion.Open()
            consulta = "SELECT idusuario FROM usuarios WHERE docnom='" & Secretaria_Nom.Text & "' AND tipo_usuario='Secretaria'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            adaptador.Fill(tabla)
            If tabla.Rows.Count = 1 Then
                panel_secretaria.Children.Clear()
                If tabla.Rows(0)(0).ToString = "1" Then
                    panel_secretaria.Children.Add(New Drpanel1)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "2" Then
                    panel_secretaria.Children.Add(New Drpanel2)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "3" Then
                    panel_secretaria.Children.Add(New Drpanel3)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "4" Then
                    panel_secretaria.Children.Add(New Drpanel4)
                    Activo()
                ElseIf tabla.Rows(0)(0).ToString = "5" Then
                    panel_secretaria.Children.Add(New Drpanel5)
                    Activo()
                End If
            Else
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "Contacte con el Administrador del Sistema, Error No.x004-1"
                DialogHost.Show(dlgshw, "RootDialog")
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
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
End Class
