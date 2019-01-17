Imports MaterialDesignThemes.Wpf
Imports MySql.Data.MySqlClient
Imports System.ComponentModel
Imports System.Data
Imports System.Windows.Threading

Public Class Administrador

    Dim WithEvents ds As New DispatcherTimer
    Dim refresh As Integer = 0
    Dim updateuser As String = String.Empty
    Dim modocerrar As Integer = 0
    Dim row As DataRowView
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim conexion1 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim conexion2 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim conexion3 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub win_move_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles win_move.MouseLeftButtonDown
        DragMove()
    End Sub

    Private Sub btn_close_Click(sender As Object, e As RoutedEventArgs) Handles btn_close.Selected
        btn_menu.IsChecked = False
        Dim dlgclshw = New MessageClsDlg
        dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
        DialogHost.Show(dlgclshw, "RootDialog")
    End Sub

    Private Sub btn_menu_Click(sender As Object, e As RoutedEventArgs) Handles btn_menu.Click
        lat_menu.SelectedIndex = -1
        btn_rtn.IsChecked = False
    End Sub

    Private Sub cls_sesion_Selected(sender As Object, e As RoutedEventArgs) Handles cls_sesion.Selected
        Dim Login As New Login
        Login.Show()
        modocerrar = 1
        Finalize()
        Close()
    End Sub

    Private Sub Administrador_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Lista()
        If Usersgrid.SelectedIndex <> -1 Then
            UpdateUserBG.IsEnabled = True
            UpdateUserBG.Badge = "Actualizar"
            AddUserB.IsEnabled = False
            AddUserB.Badge = ""
            DeletUser.IsEnabled = True
            DeletUser.Badge = "Eliminar"
        Else
            UpdateUserBG.IsEnabled = False
            UpdateUserBG.Badge = ""
            AddUserB.IsEnabled = True
            AddUserB.Badge = "Agregar"
            DeletUser.IsEnabled = False
            DeletUser.Badge = ""
        End If
        modocerrar = 0
        refresh = 0
    End Sub

    'Private Sub UserDR_Selected(sender As Object, e As RoutedEventArgs) Handles UserDR.Selected
    '    btn_menu.IsChecked = False
    '    Usuariosgeneral.IsEnabled = False
    '    Usuariosgeneral.Visibility = Visibility.Collapsed
    '    PgAdmin.IsEnabled = True
    '    PgAdmin.Visibility = Visibility.Visible
    '    PgAdmin.Children.Clear()
    '    PgAdmin.Children.Add(New UsersDoc)
    'End Sub

    'Private Sub UserAD_Selected(sender As Object, e As RoutedEventArgs) Handles UserAD.Selected
    '    btn_menu.IsChecked = False
    '    Usuariosgeneral.IsEnabled = False
    '    Usuariosgeneral.Visibility = Visibility.Collapsed
    '    PgAdmin.IsEnabled = True
    '    PgAdmin.Visibility = Visibility.Visible
    '    PgAdmin.Children.Clear()
    '    PgAdmin.Children.Add(New UsersAD)
    'End Sub

    'Private Sub UserSCR_Selected(sender As Object, e As RoutedEventArgs) Handles UserSCR.Selected
    '    btn_menu.IsChecked = False
    '    Usuariosgeneral.IsEnabled = False
    '    Usuariosgeneral.Visibility = Visibility.Collapsed
    '    PgAdmin.IsEnabled = True
    '    PgAdmin.Visibility = Visibility.Visible
    '    PgAdmin.Children.Clear()
    '    PgAdmin.Children.Add(New UsersScr)
    'End Sub

    Private Sub Usersgnl_Selected(sender As Object, e As RoutedEventArgs) Handles Usersgnl.Selected
        btn_menu.IsChecked = False
        Usuariosgeneral.IsEnabled = True
        Usuariosgeneral.Visibility = Visibility.Visible
        'PgAdmin.IsEnabled = False
        'PgAdmin.Visibility = Visibility.Collapsed
    End Sub

    Private Sub Btnactualizar_Click(sender As Object, e As RoutedEventArgs) Handles Btnactualizar.Click
        refresh = 1
        Usersgrid.SelectedItems.Clear()
        Usersgrid.SelectedIndex = -1
        Lista()
    End Sub

    Private Sub Lista()
        Try
            conexion.Open()
            consulta = "SELECT idusuario, tipo_usuario, user, docnom, apnom, tel1, tel2, tel3, codclinica FROM usuarios"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            If refresh = 1 Then
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "Lista Actualizada Exitosamente"
                DialogHost.Show(dlgshw, "RootDialog")
                refresh = 0
            ElseIf refresh = 2 Then
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "El usuario se ha Actualizado"
                DialogHost.Show(dlgshw, "RootDialog")
                refresh = 0
            ElseIf refresh = 3 Then
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "El usuario a sido Agregado"
                DialogHost.Show(dlgshw, "RootDialog")
                refresh = 0
            ElseIf refresh = 4 Then
                Dim dlgshw As New MessageDialog
                dlgshw.Message.Text = "El usuario se ha Eliminado"
                DialogHost.Show(dlgshw, "RootDialog")
                refresh = 0
            End If
            Usersgrid.ItemsSource = tabla.DefaultView
            If Usersgrid.Items.Count <> 0 Then
                Usersgrid.Columns(0).Header = "ID"
                Usersgrid.Columns(0).Width = 60
                Usersgrid.Columns(1).Header = "Tipo"
                Usersgrid.Columns(1).Width = 100
                Usersgrid.Columns(2).Header = "Usuario"
                Usersgrid.Columns(2).Width = 80
                Usersgrid.Columns(3).Header = "Nombre"
                Usersgrid.Columns(3).Width = 120
                Usersgrid.Columns(4).Header = "Apellidos"
                Usersgrid.Columns(4).Width = 120
                Usersgrid.Columns(5).Header = "Teléfono #1"
                Usersgrid.Columns(5).Width = 80
                Usersgrid.Columns(6).Header = "Teléfono #2"
                Usersgrid.Columns(6).Width = 80
                Usersgrid.Columns(7).Header = "Teléfono #3"
                Usersgrid.Columns(7).Width = 80
                Usersgrid.Columns(8).Header = "´Clínica"
                Usersgrid.Columns(8).Width = 80
            End If
            AddUserB.IsEnabled = True
            AddUserB.Badge = "Agregar"
        Catch ex As Exception
            MsgBox(ex.Message)
            refresh = 0
            Log.e("Error con excepción y Traza", ex, New StackFrame(True))
        Finally
            refresh = 0
            conexion.Close()
        End Try
    End Sub

    Private Sub Usersgrid_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles Usersgrid.SelectedCellsChanged
        If Usersgrid.SelectedIndex <> -1 Then
            UpdateUserBG.IsEnabled = True
            UpdateUserBG.Badge = "Actualizar"
            AddUserB.IsEnabled = False
            AddUserB.Badge = ""
            DeletUser.IsEnabled = True
            DeletUser.Badge = "Eliminar"
            row = Usersgrid.SelectedItem
            CodClinica.Text = row.Row.ItemArray(8).ToString
            Nameuser.Text = row.Row.ItemArray(3).ToString
            Firstuser.Text = row.Row.ItemArray(4).ToString
            user.Text = row.Row.ItemArray(2).ToString
            IDnew.Text = row.Row.ItemArray(0).ToString
            tel1.Text = row.Row.ItemArray(5).ToString
            tel2.Text = row.Row.ItemArray(6).ToString
            tel3.Text = row.Row.ItemArray(7).ToString
            TypeUser.Text = row.Row.ItemArray(1).ToString
            If TypeUser.Text = "Secretaria" Then
                CodClinica.IsEnabled = False
            ElseIf TypeUser.Text = "Administrador" Then
                CodClinica.IsEnabled = False
            Else
                CodClinica.IsEnabled = True
            End If
        Else
            UpdateUserBG.IsEnabled = False
            UpdateUserBG.Badge = ""
            AddUserB.IsEnabled = True
            AddUserB.Badge = "Agregar"
            DeletUser.IsEnabled = False
            DeletUser.Badge = ""
            CodClinica.Text = ""
            Nameuser.Text = ""
            Firstuser.Text = ""
            user.Text = ""
            Txt_newpassword.Password = ""
            IDnew.Text = ""
            tel1.Text = ""
            tel2.Text = ""
            tel3.Text = ""
            TypeUser.Text = ""
            row.CancelEdit()
        End If
    End Sub

    Private Sub Administrador_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        If modocerrar = 1 Then
            e.Cancel = False
        Else
            Dim dlgclshw = New MessageClsDlg
            dlgclshw.Message.Text = "¿Quieres cerrar el programa?"
            DialogHost.Show(dlgclshw, "RootDialog")
        End If
    End Sub

    Private Sub Actualizaruser_Click(sender As Object, e As RoutedEventArgs) Handles Actualizaruser.Click
        Dim cambiarcontraseña As Boolean
        If Txt_newpassword.Password = "" Then
            cambiarcontraseña = False
            Try
                conexion1.Open()
                updateuser = "UPDATE usuarios SET tipo_usuario='" & TypeUser.Text & "', user='" & user.Text & "', docnom='" & Nameuser.Text & "', apnom='" & Firstuser.Text & "', tel1='" & tel1.Text & "', tel2='" & tel2.Text & "', tel3='" & tel3.Text & "', codclinica='" & CodClinica.Text & "' WHERE idusuario='" & IDnew.Text & "'"
                comando = New MySqlCommand(updateuser, conexion1)
                comando.ExecuteNonQuery()
                refresh = 2
                Lista()
            Catch ex As Exception
                refresh = 0
                MessageBox.Show(ex.Message)
                Log.e("Error con excepción y traza", ex, New StackFrame(True))
            Finally
                conexion1.Close()
            End Try
        Else
            cambiarcontraseña = True
            If cambiarcontraseña = True Then
                Dim dlgshwaut As New Autentication
                DialogHost.Show(dlgshwaut, "RootDialog")
                ds.Interval = New TimeSpan(0, 0, 5)
                ds.Start()
            End If
        End If
    End Sub

    Private Sub AddUser_Click(sender As Object, e As RoutedEventArgs) Handles AddUser.Click
        If Nameuser.Text <> "" And Firstuser.Text <> "" And user.Text <> "" And Txt_newpassword.Password <> "" And IDnew.Text <> "" Then
            Try
                conexion2.Open()
                consulta = "INSERT INTO usuarios (idusuario, tipo_usuario, user, docnom, apnom, tel1, tel2, tel3, codclinica, pass) VALUES ('" & IDnew.Text & "', '" & TypeUser.Text & "', '" & user.Text & "', '" & Nameuser.Text & "', '" & Firstuser.Text & "', '" & tel1.Text & "', '" & tel2.Text & "', '" & tel3.Text & "', '" & CodClinica.Text & "', '" & Txt_newpassword.Password & "')"
                comando = New MySqlCommand(consulta, conexion2)
                comando.ExecuteNonQueryAsync()
                refresh = 3
                Lista()
            Catch ex As Exception
                refresh = 0
                MsgBox(ex.Message)
                Log.e("Error con excepción y Traza", ex, New StackFrame(True))
            Finally
                conexion2.Close()
            End Try
        Else
            Dim dlgshw As New MessageDialog
            dlgshw.Message.Text = "Error: No cumple con los datos necesarios para crear un usuario"
            DialogHost.Show(dlgshw, "RootDialog")
        End If
    End Sub

    Private Sub ds_Tick(sender As Object, e As EventArgs) Handles ds.Tick
        If My.Settings.PermitirCambio = True Then
            Try
                Dim conexion1 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
                conexion1.Open()
                updateuser = "UPDATE usuarios SET tipo_usuario='" & TypeUser.Text & "', user='" & user.Text & "', docnom='" & Nameuser.Text & "', apnom='" & Firstuser.Text & "', tel1='" & tel1.Text & "', tel2='" & tel2.Text & "', tel3='" & tel3.Text & "', codclinica='" & CodClinica.Text & "', pass='" & Txt_newpassword.Password & "' WHERE idusuario='" & IDnew.Text & "'"
                comando = New MySqlCommand(updateuser, conexion1)
                comando.ExecuteNonQuery()
                refresh = 2
                Lista()
                My.Settings.PermitirCambio = False
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Log.e("Error con excepción y traza", ex, New StackFrame(True))
            Finally
                conexion.Close()
            End Try
            ds.Stop()
        End If
    End Sub

    Private Sub DeleteUser_Click(sender As Object, e As RoutedEventArgs) Handles DeleteUser.Click
        If IDnew.Text <> "" Then
            Try
                conexion3.Open()
                consulta = "DELETE FROM usuarios WHERE usuarios.idusuario='" & IDnew.Text & "'"
                comando = New MySqlCommand(consulta, conexion3)
                comando.ExecuteNonQueryAsync()
                refresh = 4
                Lista()
            Catch ex As Exception
                refresh = 0
                MsgBox(ex.Message)
                Log.e("Error con excepción y Traza", ex, New StackFrame(True))
            Finally
                conexion3.Close()
            End Try
        Else
            Dim dlgshw As New MessageDialog
            dlgshw.Message.Text = "Error: No cumple con los datos necesarios para eliminar a este usuario"
            DialogHost.Show(dlgshw, "RootDialog")
        End If
    End Sub
End Class