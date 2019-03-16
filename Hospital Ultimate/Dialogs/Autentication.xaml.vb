Imports System.Data
Imports MySql.Data.MySqlClient

Public Class Autentication

    Dim conexion As New MySqlConnection("server=" & My.Settings.ipServer & " user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable

    Private Sub Autenticar_Click(sender As Object, e As RoutedEventArgs) Handles Autenticar.Click
        Try
            conexion.Open()
            consulta = "SELECT tipo_usuario, idusuario FROM usuarios WHERE user='" & user.Text & "' AND pass='" & pass.Password & "'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            If tabla.Rows.Count = 1 Then
                If tabla.Rows(0)(0).ToString = "Administrador" Then
                    My.Settings.PermitirCambio = True
                    Cancel.Content = "Aceptar"
                    Autenticar.Content = "Autenticado"
                    Autenticar.IsEnabled = False
                    info.Text = "Autenticado"
                Else
                    info.Text = "Inválido"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub
End Class
