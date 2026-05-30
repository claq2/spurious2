import { render, screen, waitFor } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import SubdivisionListDesktop from "./SubdivisionListDesktop";

const routerMocks = vi.hoisted(() => ({
  useRouteLoaderData: vi.fn(),
  useParams: vi.fn(),
}));

const queryMocks = vi.hoisted(() => ({
  useGetSubdivisionsByDensityQuery: vi.fn(),
}));

vi.mock("react-router-dom", async () => {
  const actual =
    await vi.importActual<typeof import("react-router-dom")>(
      "react-router-dom",
    );
  return {
    ...actual,
    useRouteLoaderData: routerMocks.useRouteLoaderData,
    useParams: routerMocks.useParams,
  };
});

vi.mock("../services/subdivisions", () => ({
  useGetSubdivisionsByDensityQuery: queryMocks.useGetSubdivisionsByDensityQuery,
}));

vi.mock("@mui/x-data-grid", () => ({
  DataGrid: ({ rows }: { rows: Array<{ id: number; name: string }> }) => (
    <div>
      <div data-testid="rows-count">{rows.length}</div>
      {rows.map((r) => (
        <div key={r.id}>{r.name}</div>
      ))}
    </div>
  ),
  gridClasses: {
    cell: "cell",
    columnHeader: "columnHeader",
  },
}));

describe("SubdivisionListDesktop", () => {
  it("renders subdivisions and selects the first item", async () => {
    const onSubdivisionChange = vi.fn();
    routerMocks.useRouteLoaderData.mockReturnValue([
      { shortName: "beer", title: "Beer", address: "" },
    ]);
    routerMocks.useParams.mockReturnValue({ id: "beer" });
    queryMocks.useGetSubdivisionsByDensityQuery.mockReturnValue({
      data: [
        {
          id: 11,
          name: "Sample Subdivision",
          population: 1000,
          requestedDensityAmount: 10,
          boundaryLink: "",
          centreCoordinates: { type: "Point", coordinates: [0, 0] },
        },
      ],
      isLoading: false,
      isFetching: false,
      isSuccess: true,
      isError: false,
    });

    render(
      <SubdivisionListDesktop onSubdivisionChange={onSubdivisionChange} />,
    );

    expect(screen.getByTestId("rows-count")).toHaveTextContent("1");
    expect(screen.getByText("Sample Subdivision")).toBeInTheDocument();

    await waitFor(() => {
      expect(onSubdivisionChange).toHaveBeenCalledWith(11);
    });
  });
});
