import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";
import About from "./About";

describe("About", () => {
  it("renders about content and github link", () => {
    render(<About />);

    expect(screen.getByRole("heading", { name: /about/i })).toBeInTheDocument();
    expect(
      screen.getByRole("link", { name: /fork it on github/i }),
    ).toHaveAttribute("href", "https://github.com/claq2/spurious2");
  });
});
