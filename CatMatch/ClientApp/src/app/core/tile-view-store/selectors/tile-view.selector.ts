import { createFeatureSelector, createSelector } from "@ngrx/store";
import { TileState } from "../models/tile.state.model";

export const tileViewKey: string = 'tileCats';

export const selectTileCats = createFeatureSelector<TileState>(tileViewKey);

export const selectCats = createSelector(
    selectTileCats,
    state => state.cats
);

export const selectIsLoading = createSelector(
    selectTileCats,
    state => state.loading
)

export const selectHasError = createSelector(
    selectTileCats,
    state => state.error
)

export const selectCount = createSelector(
    selectTileCats,
    state => state.count
)