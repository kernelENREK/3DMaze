Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Scene
    Inherits DrawableGameComponent
    Public cameraPosition As Vector3
    Public cameraTarget As Vector3
    Public projection As Matrix
    Public view As Matrix
    Public world As Matrix
    Public player As FreeCam

    Public level As VoxelObject
    Private objects As List(Of SceneObject)

    Private fov As Single

    Private wireframe As Boolean
    Private wireFrameState As RasterizerState

    Public Sub New(game As Game)
        MyBase.New(game)
        Me.objects = New List(Of SceneObject)()
        Dim state As New RasterizerState()
        state.FillMode = FillMode.WireFrame
        Me.wireFrameState = state
        Me.fov = 75.0F
    End Sub

    Public Overrides Sub Draw(gameTime As GameTime)
        MyBase.Draw(gameTime)
        Me.GraphicsDevice.SamplerStates(0) = SamplerState.PointClamp
        If Me.wireframe Then
            MyBase.GraphicsDevice.RasterizerState = Me.wireFrameState
        End If
        For Each obj2 As SceneObject In Me.objects
            If obj2.visible Then
                obj2.Draw(gameTime)
            End If
        Next
    End Sub

    Public Overrides Sub Initialize()
        MyBase.Initialize()
        Me.world = Matrix.Identity
        Me.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(Me.fov), MyBase.GraphicsDevice.Viewport.AspectRatio, 1.0F, 6000.0F)
        Me.view = Matrix.CreateLookAt(Me.cameraPosition, Me.cameraTarget, Vector3.Up)
    End Sub

    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()

        Dim cam As New FreeCam(Me)
        Me.player = cam
        Me.objects.Add(Me.player)

        'No texture provided!!
        'Dim lvl As New VoxelObject(Me, 34, 34, 18) With {.texture = MyBase.Game.Content.Load(Of Texture2D)("tileset")}
        Dim lvl As New VoxelObject(Me, 34, 34, 18)

        Me.level = lvl
        Me.level.Randomize()
        Me.objects.Add(Me.level)
    End Sub

    Public Overrides Sub Update(gameTime As GameTime)
        MyBase.Update(gameTime)
        Dim deltams As Single = CSng(gameTime.ElapsedGameTime.TotalMilliseconds) / 1000.0F
        For Each obj2 As SceneObject In Me.objects
            obj2.Update(gameTime)
        Next

        Me.view = Matrix.CreateLookAt(Me.cameraPosition, Me.cameraTarget, Vector3.Up)
        Me.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(Me.fov), MyBase.GraphicsDevice.Viewport.AspectRatio, 1.0F, 6000.0F)

        Dim kbs As KeyboardState = Keyboard.GetState()
        If kbs.IsKeyDown(Keys.F1) Then
            Me.wireframe = False
        End If
        If kbs.IsKeyDown(Keys.F2) Then
            Me.wireframe = True
        End If

        Dim ms As MouseState = Mouse.GetState()
        If (ms.RightButton = ButtonState.Pressed) AndAlso (Me.fov > 25.0F) Then
            Me.fov -= deltams * 100.0F
        ElseIf (ms.RightButton = ButtonState.Released) AndAlso (Me.fov < 90.0F) Then
            Me.fov += deltams * 100.0F
        End If
        If Me.fov > 90.0F Then
            Me.fov = 90.0F
        End If
        If Me.fov < 25.0F Then
            Me.fov = 25.0F
        End If
    End Sub
End Class
