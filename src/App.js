import React, { Fragment, useState, useCallback, useEffect } from "react";
import { Unity, useUnityContext } from "react-unity-webgl";
function App() {
  const [isGameOver, setIsGameOver] = useState(false);
  const [userName, setUserName] = useState();
  const [score, setScore] = useState();

  const { unityProvider, addEventListener, removeEventListener, sendMessage } =
    useUnityContext({
      loaderUrl: "build/public.loader.js",
      dataUrl: "build/public.data.unityweb",
      frameworkUrl: "build/public.framework.js.unityweb",
      codeUrl: "build/public.wasm.unityweb",
    });

    function handleClickSpawnEnemies() {
      console.log("AAA");
      sendMessage("GameController", "SpawnEnemies", 100);
    }

  const handleGameOver = useCallback((userName, score) => {
    console.log("GamePoint");
    setIsGameOver(true);
    setUserName(userName);
    setScore(score);
  }, []);

  const gameOver = useCallback((userName, score) => {
    console.log("GameOver");
  }, []);

  useEffect(function () {
    console.log("AOAO");
    addEventListener("GamePoint", handleGameOver);
    addEventListener("GameOver", gameOver)
    return () => {
      removeEventListener("GamePoint", handleGameOver);
      removeEventListener("GameOver", gameOver);
    };
  }, [addEventListener, removeEventListener, handleGameOver, gameOver]);
  return (
    
    <Fragment>
      <button onClick={handleClickSpawnEnemies}>Spawn Enemies</button>
      {isGameOver === true && (
        <p>{`Game Over ${userName}! You've scored ${score} points.`}</p>
      )}
      <Unity unityProvider={unityProvider} className ="unity-context"
      style={{
        height: "640px",
        width: "1080px",
        border: "2px solid black",
        background: "grey",
      }}/>
      
    </Fragment>
  );
}

export default App;