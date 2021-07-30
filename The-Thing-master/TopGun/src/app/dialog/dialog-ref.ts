import { Subject, Observable } from 'rxjs';

export class DialogRef {

    // tslint:disable-next-line: variable-name
    private readonly _afterClosed = new Subject<any>();
    afterClosed: Observable<any> = this._afterClosed.asObservable();

    constructor() { }

    close(result?: any) {
        this._afterClosed.next(result);
    }


}
