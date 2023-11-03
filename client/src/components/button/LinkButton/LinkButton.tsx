import React,{FC} from 'react';
import {Link} from 'react-router-dom';

interface Props {
    children?: React.ReactNode;
    to:string
}

const LinkButton : FC<Props> = ({
                                    children,
                                    to
                                }) => {
    return (
        <Link
            to={to}
            className="rounded-md bg-indigo-600 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
        >
            {children}
        </Link>
    );
};

export default LinkButton;