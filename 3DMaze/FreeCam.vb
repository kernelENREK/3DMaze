Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input

Public Class FreeCam
    Inherits SceneObject

    Private grav As Single
    Public Shared isFree As Boolean
    Private pitch As Single
    Private turn As Single

    Public Sub New(scene As Scene)
        MyBase.New(scene)
        MyBase.visible = False
    End Sub

    Public Overrides Sub Update(gameTime As GameTime)
        Dim speed As Single = 2000.0F
        Dim runMulti As Single = 2.0F
        Dim deltaTime As Single = CSng(gameTime.ElapsedGameTime.TotalMilliseconds) / 1000.0F

        Dim centerW As Integer = CInt(MyBase.scene.GraphicsDevice.Viewport.Width / 2)
        Dim centerH As Integer = CInt(MyBase.scene.GraphicsDevice.Viewport.Height / 2)
        Dim state As MouseState = Mouse.GetState()
        Mouse.SetPosition(centerW, centerH)

        Dim turn As Single = CSng(state.X - centerW) / 100.0F
        Dim pitch As Single = CSng(state.Y - centerH) / 100.0F

        Me.turn -= turn
        Me.pitch -= pitch

        If Me.pitch > 1.53938043021956 Then
            Me.pitch = 1.53938F
        End If
        If Me.pitch < -1.53938043021956 Then
            Me.pitch = -1.53938F
        End If

        Dim vector As Vector3 = Vector3.Forward
        Dim vector2 As Vector3 = Vector3.Forward
        Dim vector3Pos As Vector3 = Vector3.Left

        If Not isFree Then
            vector2 = Vector3.Transform(vector, Matrix.CreateRotationY(Me.turn))
        Else
            vector2 = Vector3.Transform(vector, Matrix.CreateRotationX(Me.pitch) * Matrix.CreateRotationY(Me.turn))
        End If

        vector = Vector3.Transform(vector, Matrix.CreateRotationX(Me.pitch) * Matrix.CreateRotationY(Me.turn))
        vector3Pos = Vector3.Transform(vector3Pos, Matrix.CreateRotationY(Me.turn))
        MyBase.move = Vector3.Zero

        Dim kbs As KeyboardState = Keyboard.GetState()
        If kbs.IsKeyDown(Keys.W) Then
            MyBase.move += vector2 * If(kbs.IsKeyDown(Keys.LeftShift), (speed * runMulti), speed)
        End If
        If kbs.IsKeyDown(Keys.S) Then
            MyBase.move -= vector2 * If(kbs.IsKeyDown(Keys.LeftShift), (speed * runMulti), speed)
        End If
        If kbs.IsKeyDown(Keys.A) Then
            MyBase.move += vector3Pos * If(kbs.IsKeyDown(Keys.LeftShift), (speed * runMulti), speed)
        End If
        If kbs.IsKeyDown(Keys.D) Then
            MyBase.move -= vector3Pos * If(kbs.IsKeyDown(Keys.LeftShift), (speed * runMulti), speed)
        End If
        If kbs.IsKeyDown(Keys.F3) Then
            isFree = False
            MyBase.collide = True
        End If
        If kbs.IsKeyDown(Keys.F4) Then
            isFree = True
            MyBase.collide = True
        End If
        If kbs.IsKeyDown(Keys.F12) Then
            isFree = True
            MyBase.collide = False
        End If
        If Not isFree Then
            If kbs.IsKeyDown(Keys.Q) Then
                Me.grav = -2000.0F
            End If
            If kbs.IsKeyDown(Keys.Space) AndAlso MyBase.onGround Then
                Me.grav = -5000.0F
                MyBase.onGround = False
            Else
                Me.grav += deltaTime * 9000.0F
            End If
            Me.move.Y -= Me.grav
        End If

        MyBase.Update(gameTime)

        If Me.lastMove.Y = 0.0F Then
            Me.grav = 0.0F
        End If
        MyBase.scene.cameraPosition = MyBase.position
        MyBase.scene.cameraTarget = MyBase.scene.cameraPosition + vector

    End Sub
End Class
