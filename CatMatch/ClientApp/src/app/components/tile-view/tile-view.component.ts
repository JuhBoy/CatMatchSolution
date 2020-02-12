import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { RequestCats } from "src/app/core/tile-view-store/actions/tile-view.actions";
import { Observable } from "rxjs";
import { Cat } from "src/app/core/models/cat.model";
import { selectCats } from "src/app/core/tile-view-store/selectors/tile-view.selector";

@Component({
    selector: 'tile-view',
    styleUrls: ['./tile-view.component.scss'],
    templateUrl: './tile-view.component.html'
})
export class TileViewComponent implements OnInit {

    public cats$: Observable<Cat[]>;
    
    public constructor(private store: Store<any>) { }

    public ngOnInit(): void {
        this.cats$ = this.store.select(selectCats);
        this.store.dispatch(RequestCats({}));
    }
}