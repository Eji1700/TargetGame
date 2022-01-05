module Game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Input 
open System

type TargetGame () as x =
    inherit Game()
 
    do x.Content.RootDirectory <- "Content"
    do x.IsMouseVisible <- true
    let graphics = new GraphicsDeviceManager(x)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    let mutable targetSprite = Unchecked.defaultof<Texture2D>
    let mutable crosshairsSprite = Unchecked.defaultof<Texture2D>
    let mutable backgroundSprite = Unchecked.defaultof<Texture2D>
    let mutable gameFont = Unchecked.defaultof<SpriteFont>
    
    let mutable targetPosition = Vector2(300f, 300f)
    let targetRadius = 45f

    let mutable mState = Unchecked.defaultof<MouseState>
    let mutable mRelease = true
    let mutable Score = 0

    let quitGame (game: TargetGame) =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) then 
            game.Exit()

    // let (|KeyDown|_|) k (state: KeyboardState) =
    //     if state.IsKeyDown k then Some() else None

    // let getMovementVector = function
    //     | KeyDown Keys.W -> Vector2(0f,-1f)
    //     | _ -> Vector2.Zero

    override x.Initialize() =
        spriteBatch <- new SpriteBatch(x.GraphicsDevice)
        base.Initialize()

    override this.LoadContent() =
        targetSprite <- this.Content.Load<Texture2D>("target") 
        crosshairsSprite <- this.Content.Load<Texture2D>("crosshairs") 
        backgroundSprite <- this.Content.Load<Texture2D>("sky") 
        gameFont <- this.Content.Load<SpriteFont>("galleryFont")
 
    override this.Update (gameTime) =
        quitGame this

        mState <- Mouse.GetState()
        let mouseTargetDist =  Vector2.Distance(targetPosition, mState.Position.ToVector2())
        match mState.LeftButton, mRelease with 
        | ButtonState.Pressed, true -> 
            if mouseTargetDist <= targetRadius then 
                Score <- Score + 1
                let rand = Random()
                targetPosition.X <- rand.Next(0, graphics.PreferredBackBufferWidth) |> float32
                targetPosition.Y <- rand.Next(0, graphics.PreferredBackBufferHeight) |> float32
            mRelease <- false
        | ButtonState.Released, false -> 
            mRelease <- true
        | _ -> ()

        base.Update(gameTime)

    override this.Draw (gameTime) =
        x.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin()
        spriteBatch.Draw(backgroundSprite, Vector2(0f, 0f), Color.White)
        spriteBatch.DrawString(gameFont, Score.ToString(), Vector2(100f,100f), Color.White)
        spriteBatch.Draw(targetSprite, Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White)
        spriteBatch.End()

        base.Draw(gameTime)