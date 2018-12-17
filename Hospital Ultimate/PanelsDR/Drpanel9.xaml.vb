Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Threading

Public Class Drpanel9

    Dim WithEvents ds As New DispatcherTimer
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim update_state As String = String.Empty

    Private Sub Drpanel1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ds.Interval = New TimeSpan(0, 0, 20)
        ds.Start()
        estado.IsChecked = True
        Lista()
    End Sub

    Public Sub Lista()
        Try
            conexion.Open()
            consulta = "SELECT Tiket FROM pacientes WHERE idDoctor='14' AND estado_paciente='1'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            If tabla.Rows.Count <> 0 Then
                list_pacientes.SelectedIndex = 0
                list_pacientes.ItemsSource = tabla.DefaultView
                Dim row As DataRowView
                row = list_pacientes.SelectedItem
                paciente.Text = row.Row.ItemArray(0).ToString
            Else
                list_pacientes.ItemsSource = tabla.DefaultView
                paciente.Text = "0"
            End If
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

    Private Sub ds_Tick(sender As Object, e As EventArgs) Handles ds.Tick
        Lista()
    End Sub

    Private Sub cliente_sig_Click(sender As Object, e As RoutedEventArgs) Handles cliente_sig.Click
        list_pacientes.SelectedIndex += 1
        Dim cuentapacientes As Integer
        cuentapacientes = (list_pacientes.Items.Count - 1)
        If list_pacientes.SelectedIndex < cuentapacientes Then
            MsgBox("Hay más registros")
        ElseIf list_pacientes.SelectedIndex = cuentapacientes Then
            MsgBox("Último registro")
        End If
    End Sub
End Class
