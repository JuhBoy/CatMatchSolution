import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType, Effect } from '@ngrx/effects';
import { CatService } from "../../services/cat.service";
import { TileViewActions, RequestCats, LoadCatsFailed, ReceiveCats } from "../actions/tile-view.actions";
import { tap, take } from "rxjs/operators";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { CommonResponse } from "../../models/common.response.model";
import { Cat } from "../../models/cat.model";

@Injectable()
export class TileViewEffects {

    constructor(
        private store: Store<any>,
        private actions$: Actions,
        private catService: CatService
    ) { }

    public requestCats$ = createEffect(() => this.actions$.pipe(
        ofType<any>(TileViewActions.RequestCats),
        tap(_ => {
            this.catService.getAll().pipe(take(1)).subscribe((rsp: CommonResponse<Cat>) => {
                if (rsp.internalCode == 200) {
                    this.store.dispatch(ReceiveCats({ cats: rsp.payload.cats, count: rsp.payload.count }));
                } else {
                    this.store.dispatch(LoadCatsFailed({ exception: rsp.payload, message: rsp.message }));
                }
            }, error => {
                this.store.dispatch(LoadCatsFailed({ exception: '', message: 'Network Error' }));
            });
        }),
    ), { dispatch: false });
}