import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import SubdivisionList from "./SubdivisionList";

const mediaQueryMock = vi.hoisted(() => ({
  useMediaQuery: vi.fn(),
}));

vi.mock("@mui/material/useMediaQuery", () => ({
  default: mediaQueryMock.useMediaQuery,
}));

vi.mock("./SubdivisionListMobile", () => ({
  default: () => <div>Mobile List</div>,
}));

vi.mock("./SubdivisionListDesktop", () => ({
  default: () => <div>Desktop List</div>,
}));

describe("SubdivisionList", () => {
  it("renders mobile list when on small screen", () => {
    mediaQueryMock.useMediaQuery.mockReturnValue(true);

    render(<SubdivisionList onSubdivisionChange={vi.fn()} />);

    expect(screen.getByText("Mobile List")).toBeInTheDocument();
  });

  it("renders desktop list when not on small screen", () => {
    mediaQueryMock.useMediaQuery.mockReturnValue(false);

    render(<SubdivisionList onSubdivisionChange={vi.fn()} />);

    expect(screen.getByText("Desktop List")).toBeInTheDocument();
  });
});
