import { render, screen } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { describe, expect, it, vi } from "vitest";
import NavBar from "./NavBar";

const routerMocks = vi.hoisted(() => ({
  useRouteLoaderData: vi.fn(),
  useNavigation: vi.fn(),
  useLocation: vi.fn(),
}));

vi.mock("react-router-dom", async () => {
  const actual =
    await vi.importActual<typeof import("react-router-dom")>(
      "react-router-dom",
    );
  return {
    ...actual,
    useRouteLoaderData: routerMocks.useRouteLoaderData,
    useNavigation: routerMocks.useNavigation,
    useLocation: routerMocks.useLocation,
  };
});

describe("NavBar", () => {
  it("renders navigation links for sample density data", () => {
    routerMocks.useRouteLoaderData.mockReturnValue([
      { shortName: "beer", title: "Beer", address: "" },
      { shortName: "wine", title: "Wine", address: "" },
    ]);
    routerMocks.useNavigation.mockReturnValue({ state: "idle" });
    routerMocks.useLocation.mockReturnValue({ pathname: "/beer" });

    render(
      <MemoryRouter>
        <NavBar />
      </MemoryRouter>,
    );

    expect(screen.getByText("Spurious Alcohol Statistics")).toBeInTheDocument();
    expect(screen.getAllByText("Beer").length).toBeGreaterThan(0);
    expect(screen.getAllByText("Wine").length).toBeGreaterThan(0);
    expect(screen.getAllByText("About").length).toBeGreaterThan(0);
  });

  it("renders loading state while navigation is loading", () => {
    routerMocks.useRouteLoaderData.mockReturnValue([
      { shortName: "beer", title: "Beer", address: "" },
    ]);
    routerMocks.useNavigation.mockReturnValue({ state: "loading" });
    routerMocks.useLocation.mockReturnValue({ pathname: "/beer" });

    render(
      <MemoryRouter>
        <NavBar />
      </MemoryRouter>,
    );

    expect(
      screen.getByRole("heading", { name: /loading!/i }),
    ).toBeInTheDocument();
  });
});
