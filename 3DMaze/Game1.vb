Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Game1
    Inherits Game

    Private graphics As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch

    Private hud As Hud
    Private scene As Scene

    Public Sub New()
        graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"

        graphics.PreferredBackBufferHeight = 600
        graphics.PreferredBackBufferWidth = 900

        Me.scene = New Scene(Me)
        Me.hud = New Hud(Me)

        MyBase.Components.Add(Me.scene)
        MyBase.Components.Add(Me.hud)
        MyBase.Components.Add(New dbgInfo(Me))
    End Sub

    Protected Overrides Sub Initialize()
        MyBase.Initialize()
    End Sub

    Protected Overrides Sub LoadContent()
        spriteBatch = New SpriteBatch(GraphicsDevice)
    End Sub

    Protected Overrides Sub UnloadContent()
    End Sub

    Protected Overrides Sub Update(ByVal gameTime As GameTime)
        If (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed) OrElse Keyboard.GetState.IsKeyDown(Keys.Escape) Then
            Me.Exit()
        End If

        MyBase.Update(gameTime)
    End Sub

    Protected Overrides Sub Draw(ByVal gameTime As GameTime)
        GraphicsDevice.Clear(Color.Black)
        MyBase.Draw(gameTime)
    End Sub

End Class
