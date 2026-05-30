import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";
import { staticRoutes } from "./staticRoutes";

describe("staticRoutes", () => {
  it("contains the about route element", () => {
    render(staticRoutes[0].element);

    expect(screen.getByRole("heading", { name: /about/i })).toBeInTheDocument();
  });
});
