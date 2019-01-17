Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Windows.Threading

Public Class Drpanel4

    Dim index As Integer = -1
    Dim conexion1 As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim row As DataRowView
    Dim WithEvents ds As New DispatcherTimer
    Dim conexion As New MySqlConnection("server=192.168.1.90; user=TheAlejandRo; password=Tech.Code; database=dbturnos")
    Dim consulta As String = String.Empty
    Dim comando As MySqlCommand
    Dim adaptador As MySqlDataAdapter
    Dim tabla As New DataTable
    Dim update_state As String = String.Empty

    Private Sub Drpanel4_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ds.Interval = New TimeSpan(0, 0, 1)
        ds.Start()
        estado.IsChecked = True
        Lista()
    End Sub

    Public Sub Lista()
        Try
            conexion.Open()
            consulta = "SELECT Tiket FROM pacientes WHERE idDoctor='9' AND estado_paciente='0' OR estado_paciente='1'"
            comando = New MySqlCommand(consulta, conexion)
            adaptador = New MySqlDataAdapter(comando)
            tabla.Clear()
            adaptador.Fill(tabla)
            If tabla.Rows.Count <> 0 Then
                list_pacientes.ItemsSource = tabla.DefaultView
                list_pacientes.SelectedIndex = index
            Else
                index = -1
                list_pacientes.SelectedItems.Clear()
                list_pacientes.SelectedIndex = -1
                list_pacientes.ItemsSource = tabla.DefaultView
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub estado_Checked(sender As Object, e As RoutedEventArgs) Handles estado.Checked
        Activo()
        txt_estado.Text = "Conectado"
    End Sub

    Private Sub estado_Unchecked(sender As Object, e As RoutedEventArgs) Handles estado.Unchecked
        Inactivo()
        txt_estado.Text = "Desconectado"
    End Sub

    Private Sub Activo()
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='1' WHERE idusuario='9'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
            list_pacientes.IsEnabled = True
            cliente_sig.IsEnabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub Inactivo()
        Try
            conexion.Open()
            update_state = "UPDATE usuarios SET estado='0' WHERE idusuario='9'"
            comando = New MySqlCommand(update_state, conexion)
            comando.ExecuteNonQuery()
            list_pacientes.IsEnabled = False
            cliente_sig.IsEnabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Log.e("Error con excepción y Traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
    End Sub

    Private Sub ds_Tick(sender As Object, e As EventArgs) Handles ds.Tick
        Lista()
    End Sub

    Private Sub cliente_sig_Click(sender As Object, e As RoutedEventArgs) Handles cliente_sig.Click
        Try
            conexion.Open()
            Dim estadopaciente As String = String.Empty
            estadopaciente = "UPDATE pacientes SET estado_paciente='2' WHERE tiket='" & paciente.Text & "'"
            comando = New MySqlCommand(estadopaciente, conexion)
            comando.ExecuteNonQuery()
            cliente_sig.Content = "SIGUIENTE"
            cliente_sig.Width = 125
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.e("Error con excepción y traza", ex, New StackFrame(True))
        Finally
            conexion.Close()
        End Try
        list_pacientes.SelectedIndex += 1
        index = list_pacientes.SelectedIndex
    End Sub

    Private Sub list_pacientes_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles list_pacientes.SelectedCellsChanged
        If list_pacientes.SelectedIndex <> -1 Then
            row = list_pacientes.SelectedItem
            paciente.Text = row.Row.ItemArray(0).ToString
            Try
                conexion1.Open()
                Dim estadopac As String = String.Empty
                estadopac = "UPDATE pacientes SET estado_paciente='1' WHERE tiket='" & paciente.Text & "'"
                comando = New MySqlCommand(estadopac, conexion1)
                comando.ExecuteNonQuery()
                cliente_sig.Content = "Marcar como Atendido"
                cliente_sig.Width = 190
            Catch ex As Exception
                MsgBox(ex.Message)
                Log.e("Error con excepción y traza", ex, New StackFrame(True))
            Finally
                conexion1.Close()
            End Try
        Else
            paciente.Text = "0"
        End If
    End Sub
End Class
