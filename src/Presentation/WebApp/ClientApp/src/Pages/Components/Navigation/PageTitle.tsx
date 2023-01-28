import React from 'react';
import ReactDOM from 'react-dom';

interface defaultProps {
    title: string
}
let titleElement: HTMLElement;
const PageTitle = (props: defaultProps) => {

    titleElement = document.getElementsByTagName('title')[0] as HTMLElement;

    const { title } = props
    let pageTitle = !title ? 'Xploring Me' : title + ' :: Xploring Me';

    return (
        ReactDOM.createPortal(
            pageTitle || '', titleElement
        )
    );
}

export default React.memo(PageTitle);
