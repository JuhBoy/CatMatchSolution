import { createAction, props } from "@ngrx/store";
import { Cat } from "../../models/cat.model";

export enum TileViewActions {
    RequestCats = "[Tile View] Request Cats",
    ReceiveCats = "[Tile View] Receive Cats",
    LoadCatsFailed = "[Tile View] Load Failed"
}

export interface RequestCatsPayload { }

export interface ReceiveCatsPayload {
    cats: Cat[];
    count: number;
}

export interface LoadCatsFailedPayload {
    exception: any;
    message: string
}

export const RequestCats = createAction(
    TileViewActions.RequestCats,
    props<RequestCatsPayload>()
)

export const ReceiveCats = createAction(
    TileViewActions.ReceiveCats,
    props<ReceiveCatsPayload>()
)

export const LoadCatsFailed = createAction(
    TileViewActions.LoadCatsFailed,
    props<LoadCatsFailedPayload>()
)