Imports System.Data
Imports MySql.Data.MySqlClient

Public Class Drpanel9

    Dim conexion As New MySqlConnection("host=127.0.0.1; user=root; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim update_state As String = String.Empty

    Private Sub Drpanel1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        estado.IsChecked = True
        Lista()
    End Sub

    Public Sub Lista()
        Try
            conexion.Open()
            consulta = "SELECT Tiket FROM pacientes WHERE idDoctor='14'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            adaptador.Fill(tabla)
            list_pacientes.ItemsSource = tabla.DefaultView
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub estado_Checked(sender As Object, e As RoutedEventArgs) Handles estado.Click
        If estado.IsChecked = True Then
            Activo()
            txt_estado.Text = "Conectado"
        Else
            Inactivo()
            txt_estado.Text = "Desconectado"
        End If
    End Sub

    Private Sub Activo()
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='1' WHERE idusuario='14'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub Inactivo()
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='0' WHERE idusuario='14'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            conexion.Close()
        End Try
    End Sub
End Class
