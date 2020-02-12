import { TileState } from "../models/tile.state.model";
import { Action, createReducer, on } from "@ngrx/store";
import { RequestCats, ReceiveCats, ReceiveCatsPayload, LoadCatsFailed, LoadCatsFailedPayload } from "../actions/tile-view.actions";

export const tileInitialState: TileState = {
    cats: [],
    count: 0,
    error: false,
    loading: false
}

const tileReducer = createReducer(
    tileInitialState,
    on(RequestCats, state => ({ ...tileInitialState, loading: true })),
    on(ReceiveCats, (state, catsPayload: ReceiveCatsPayload) => ({
        ...state,
        loading: false,
        count: catsPayload.count,
        cats: catsPayload.cats
    })),
    on(LoadCatsFailed, (state, failedPayload: LoadCatsFailedPayload) => {
        return {
            ...state,
            loading: false,
            error: true
        };
    })
)

export function reducer(state: TileState | undefined, action: Action) {
    return tileReducer(state, action);
}