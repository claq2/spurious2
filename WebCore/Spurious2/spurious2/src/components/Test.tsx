import React, { useEffect } from "react";
import { useRouteLoaderData } from "react-router-dom";

const Test = () => {
  const ds = useRouteLoaderData("root");
  useEffect(() => {
    console.log("ds in test", ds);
  }, [ds]);
  return <div>Test</div>;
};

export default Test;
